import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homefrm',
  templateUrl: './homefrm.component.html',
  styleUrls: ['./homefrm.component.css']
})
export class HomefrmComponent implements OnInit {
  ifinregister=false;
  constructor() { }
  ngOnInit(): void {
  
  }
  registerTrigger(){
   this.ifinregister=!this.ifinregister; 
  }

  cancelRegisterMode(event: boolean){
this.ifinregister=event;
  }
}
