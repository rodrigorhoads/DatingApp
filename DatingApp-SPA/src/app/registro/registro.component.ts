import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})

export class RegistroComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  registrar() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Registrado com sucesso!');
    }, error => {
        this.alertify.error(error);
      });
  }

  cancelar() {
    this.cancelRegister.emit(false);
    this.alertify.warning('Cancelado');
  }

}
