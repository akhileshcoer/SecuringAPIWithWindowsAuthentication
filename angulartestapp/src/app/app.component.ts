import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  policyNotFound = true;
  policies: string[] = [];

  /**
   *
   */
  constructor(private http: HttpClient) {
    if (!sessionStorage.getItem('policies')) {
      this.policyNotFound = true;
    } else {
      this.policyNotFound = false;
    }
  }

  makeCall(): void {
    this.http
      .get<string[]>('http://localhost:55173/api/resource/myroles')
      .subscribe(p => (this.policies = p));
  }
}
