import { Component, OnInit } from '@angular/core';
import { ConfirmService } from 'src/app/_bsCommon/confirm.service';

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.scss'],
  providers:[ConfirmService]
})
export class SubcategoryComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
