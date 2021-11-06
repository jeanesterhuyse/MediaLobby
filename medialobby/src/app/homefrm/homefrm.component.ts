import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homefrm',
  templateUrl: './homefrm.component.html',
  styleUrls: ['./homefrm.component.css']
})
export class HomefrmComponent implements OnInit {
  registerMode=false;

  constructor() { }

  ngOnInit(): void {
  
  }
  registerToggle(){
   this.registerMode=!this.registerMode; 
  }

  cancelRegisterMode(event: boolean){
this.registerMode=event;
  }
}
