import { Component, Input, OnInit } from '@angular/core';
import { Category } from '../../apps/models/category';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {

  @Input() category : Category;
  constructor() { }

  ngOnInit() {
  }

}
