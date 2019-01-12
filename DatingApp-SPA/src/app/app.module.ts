import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import {NgxGalleryModule} from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';


import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { from } from 'rxjs';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegistroComponent } from './registro/registro.component';
import { ErrorInterceptorProvide } from './_services/error.Interceptor';
import { AlertifyService } from './_services/alertify.service';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import {TimeAgoPipe} from 'time-ago-pipe';

import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListResolver } from './resolver/member-list.resolver';
import { MemberDetailResolver } from './resolver/member-detail.resolver.1';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './resolver/member-edit.resolver.2';
import { PreventUnsavedChanges } from './_guards/prevents-unsaved-changes-guard';
import { PhotoeditComponent } from './members/photoedit/photoedit.component';
import { ListResolver } from './resolver/lists.resolver';



   export function tokenGetter() {
      return localStorage.getItem('token');
   }


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegistroComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoeditComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot( ),
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
          tokenGetter: tokenGetter,
          whitelistedDomains: ['localhost:5000'],
          blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvide,
      AlertifyService,
      AuthGuard,
      UserService,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditResolver,
      PreventUnsavedChanges,
      ListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
