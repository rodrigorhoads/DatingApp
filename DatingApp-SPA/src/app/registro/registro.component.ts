import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})

export class RegistroComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  user: any = {};
  model: any = {};
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService,
               private alertify: AlertifyService,
               private fb: FormBuilder,
               private router:  Router) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass : 'theme-red'
    };
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      genero: ['male'],
      username: ['', Validators.required],
      conhecidoComo: ['', Validators.required],
      nascimento: [null, Validators.required],
      cidade: ['', Validators.required],
      pais: ['', Validators.required],
      senha: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

    passwordMatchValidator(g: FormGroup) {
      return g.get('senha').value === g.get('confirmPassword').value ? null : {'mismatch' : true};
    }

  registrar() {
    if (this.registerForm.valid) {
        this.user = Object.assign({}, this.registerForm.value);
        this.authService.register(this.user).subscribe(() => {
          this.alertify.success('Registro realizado com suceso!');
        }, error => {
          this.alertify.error(error);
        }, () => {
          this.authService.login(this.user).subscribe(() => {
            this.router.navigate(['/members']);
          });
        });
      }
    }

  cancelar() {
    this.cancelRegister.emit(false);
    this.alertify.warning('Cancelado');
  }

}
