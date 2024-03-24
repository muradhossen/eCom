import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-bs-confirm-modal',
  templateUrl: './bs-confirm-modal.component.html',
  styleUrls: ['./bs-confirm-modal.component.css']
})
export class BsConfirmModalComponent implements OnInit {
  
  title : string;
  message : string;
  btnOkText : string;
  btnCancelText : string;
  result : boolean;


  constructor(public bsModalRef : BsModalRef) { }

  ngOnInit() {
  }
  confirm(){
    this.result = true;
    this.bsModalRef.hide();

  }

  decline(){
    this.result = false;
    this.bsModalRef.hide();
  }
}
