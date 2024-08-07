import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SweetAlert2LoaderService } from '@sweetalert2/ngx-sweetalert2';
import { AuthService } from '../services/auth.service';


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
  ],
  exports:[
    LoginComponent
  ],
  providers:[
    AuthService
  ]
})
export class AuthModule { }
