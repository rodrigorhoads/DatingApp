import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Photo } from 'src/app/_models/photo';


@Component({
  selector: 'app-photoedit',
  templateUrl: './photoedit.component.html',
  styleUrls: ['./photoedit.component.css']
})
export class PhotoeditComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getMemberPhotochange = new EventEmitter<string>();
   uploader: FileUploader;
   hasBaseDropZoneOver = false;
   baseUrl = environment.apiUrl;
   currentMainPhoto: Photo;

   constructor(private authService: AuthService, private userService: UserService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }


  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader(
      {
        url: this.baseUrl + 'users/' + this.authService.decodeToken.nameid + '/photos',
         authToken: 'Bearer ' + localStorage.getItem('token'),
         isHTML5: true,
         allowedFileType: ['image'],
         removeAfterUpload: true,
         autoUpload: false,
         maxFileSize: 10 * 1024 * 1024
      });

      this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; } ;

      this.uploader.onSuccessItem = (item, response, status, header) => {
        if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          DateAdd: res.DateAdd,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
        if (photo.isMain) {
          this.authService.changeMemberPhoto(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodeToken.nameid, photo.id)
    .subscribe(() => {
      this.currentMainPhoto = this.photos.filter(p => p.isMain === true)[0];
      this.currentMainPhoto.isMain = false;
      photo.isMain = true;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
      this.alertify.success('Foto principal alterada com sucesso!');
    }, error => {
      this.alertify.error(error);
    });
  }

  deletePhoto(id: number){
    this.alertify.confirm('Você tem certeza que quer deletar essa foto', () => {
      this.userService.deletePhoto(this.authService.decodeToken.nameid, id)
      .subscribe(() => {
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alertify.success('A foto foi deletada com sucesso.');
      }, error => {
        this.alertify.error('Falha ao deletar a foto.');
      });
    });
  }
}
