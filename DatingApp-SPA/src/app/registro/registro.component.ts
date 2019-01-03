import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  registrar() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Registrado com sucesso!');
    }, error => {
        console.log(error);
      });
  }

  cancelar() {
    this.cancelRegister.emit(false);
    console.log('Cancelado');
  }

}
