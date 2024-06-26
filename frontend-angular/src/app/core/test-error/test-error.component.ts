import { Component } from '@angular/core';
import { Client } from '../../api/http-client';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})

export class TestErrorComponent {

  constructor(private _client: Client) { }

  get404Error() {
    this._client.v1BuggyNotFound().subscribe({
      next: respoce => console.log(respoce),
      error: error => console.log(error),
    })
  }

  get500Error() {
    this._client.v1BuggyServerError().subscribe({
      next: respoce => console.log(respoce),
      error: error => console.log(error),
    })
  }

  get400Error() {
    this._client.v1BuggyBadRequest().subscribe({
      next: respoce => console.log(respoce),
      error: error => console.log(error),
    })
  }

  get400ValidationError() {
    this._client.v1BuggyValidationProblem().subscribe({
      next: respoce => console.log(respoce),
      error: error => console.log(error)
    })
  }
}
