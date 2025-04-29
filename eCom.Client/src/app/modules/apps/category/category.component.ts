import { Component, OnInit } from '@angular/core';
import { CreateButtonSetting, PageInfoService } from 'src/app/_metronic/layout';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  constructor(private pageInfoService : PageInfoService) { }

  ngOnInit() {
    this.pageInfoService.setToolbarCreateBtnSettings(new CreateButtonSetting('/manage/categories/create',true));
  }

}
