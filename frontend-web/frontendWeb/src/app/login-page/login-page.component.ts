import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  constructor(private router: Router) { }



  onSubmit() {
    if (this.loginForm.value.username !== 'brewer' || this.loginForm.value.password !== 'brewer') {
      alert('Incorrect username or password!');
      return;
    }
    localStorage.setItem('currentUser', this.loginForm.value.username);
    alert('Login successful!');
    this.loginForm.reset();
    this.router.navigate(['/popupbar']);
  }
}
