import { Component, OnInit } from '@angular/core';
import { ModalConfig } from '../../_metronic/partials';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateButtonSetting, PageInfoService } from 'src/app/_metronic/layout';
 
 
 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {

  constructor(private pageInfoService : PageInfoService) {}

  ngOnInit(): void {
    this.pageInfoService.setToolbarCreateBtnSettings(new CreateButtonSetting('/dashboard',false));
  } 
}
