import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryService } from '../apps/services/category.service'; 
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Hierarchy } from '../apps/models/category copy'; 
 

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {

  hierarchy : Hierarchy[] = []; 
 

 
  slides: {image: string; text?: string}[] = [
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'},
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'},
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'} 
  ];
 
  customOptions: OwlOptions = {
    loop: true,
    autoplay: true,
    mouseDrag: true,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 500, 
    navText: ['<i class="fa-solid fa-chevron-left"></i>', '<i class="fa-solid fa-chevron-right"></i>'], 
    responsive: {
      0: {
        items: 1
      } 
    },
    nav: true, 
  } 
 
  menuSubcategories: Hierarchy[] = [];

  constructor(private categoryService : CategoryService,
    private cdr: ChangeDetectorRef) { }

  ngOnInit() { 
    this.loadCategoryHierarchy();
    
  }

 
  loadCategoryHierarchy() {
    this.categoryService.getCategoryHierarchy(7).subscribe(res => {
      this.hierarchy = res; 

      this.cdr.detectChanges();
    });
  }

  renderData(data: any[]): string {
    let html = '<ul>';
    data.forEach(item => {
      html += `<li>${item.key} - ${item.value} - ${item.code}`;
      if (item.child && item.child.length > 0) {
        html += this.renderData(item.child); // Recursively call renderData
      }
      html += '</li>';
    });
    html += '</ul>';
    return html;
  }

  setMenuSubcategory(item : Hierarchy){
    this.menuSubcategories = item.child;
  } 

}
