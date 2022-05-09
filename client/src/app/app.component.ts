import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App - Siva Kumar M.K.';
  users: any;

  constructor(private http: HttpClient) {

  }
  ngOnInit() {
    this.getUsers();
  }


  getUsers() {
    this.http.get('https://localhost:7020/api/users').subscribe(resp => {
      this.users = resp;
    }, error => {
      console.log(error);
    })
  }
}