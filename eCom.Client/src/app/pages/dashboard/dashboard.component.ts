import { Component, OnInit } from '@angular/core';
import { ModalConfig } from '../../_metronic/partials';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
 
 
 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  modalConfig: ModalConfig = {
    modalTitle: 'Modal title',
    dismissButtonLabel: 'Submit',
    closeButtonLabel: 'Cancel'
  };
 
  validateForm!: FormGroup;
  submitForm(): void {
    console.log('submit', this.validateForm.value);
  }
  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }

  async openModal() { 
  }
}
