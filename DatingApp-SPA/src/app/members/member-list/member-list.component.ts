import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { JSONP_ERR_NO_CALLBACK } from '@angular/common/http/src/jsonp';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  genderList = [{value: 'male', display: 'males'}, {value: 'female', display: 'females'}];
  userParams: any = {};
  pagination: Pagination;


  constructor(private userService: UserService, private alertify: AlertifyService,
    private router: ActivatedRoute) { }

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });

    this.userParams.genero = this.user.genero === 'female' ? 'male' : 'femael';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.orderBy = 'ultimaAtividade';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.genero = this.user.genero === 'female' ? 'male' : 'female';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe((res: PaginatedResult<User[]>) => {
      this.users = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }
}
