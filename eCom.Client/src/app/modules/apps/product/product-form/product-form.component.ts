import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Subscription, debounceTime } from 'rxjs';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { SubcategoryService } from '../../services/subcategory.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {


  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  productForm: FormGroup;
  productImage: File;
  productImageUrl = environment.defaultItemImagePath;
  id: number;
  isEdit: boolean;
  category: Category = new Category();

  subcategories: Category[] = [];

  constructor(private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
    private subcategoryService : SubcategoryService) {

    const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => {

        this.isLoading = res

      });
    this.unsubscribe.push(loadingSubscr);
  }

  ngOnInit() {

    this.loadSubCategories();
    
    this.route.params.subscribe(params => {

      this.id = params['id'];
      if (this.id) {
        this.isEdit = true;
        this.getCategory(this.id);
      }
    });

    this.initForm();

  }


  initForm() {
    this.productForm = this.fb.group({
      
      code: [''],
      details: [ '',
      Validators.compose([
        Validators.maxLength(1000)
      ])],
      usp: [''],
      subCategoryId: ['', Validators.compose([Validators.required])],
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.maxLength(55),
        ]),
      ],
      description: [
        '',
        Validators.compose([
          Validators.maxLength(1000)
        ])
      ],
      image: ''
    });
  }

  submit() {
     
    const product = new Product();
    product.name = this.productForm.get('name')?.value;
    product.description = this.productForm.get('description')?.value; 
    product.image = this.productImage;
    if (!this.productImage) {
      product.imageUrl = this.category.imageUrl;
    } 
    product.code = this.productForm.get('code')?.value; 
    product.details = this.productForm.get('details')?.value; 
    product.usp = this.productForm.get('usp')?.value; 
    product.subCategoryId = this.productForm.get('subCategoryId')?.value; 

    if (this.isEdit) {
      this.productService.updateProduct(this.id,product).subscribe(res => {
        this.router.navigate(['/manage/products']);
      });
    }
    else {
      this.productService.createProduct(product).subscribe(res => {
        this.router.navigate(['/manage/products']);
      });
    }
  }


  // Method to handle file selection
  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.productImage = file;

      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.productImageUrl = e.target.result;
        this.cdr.detectChanges();
      };



    }
  }

  getCategory(id: number) {
    this.productService.getProduct(id).subscribe(product => {
 
      this.category = product; 
      this.productForm.patchValue(this.category);
      this.productImageUrl = this.category.imageUrl ?? this.productImageUrl;

      this.cdr.detectChanges();
    });

  }
  
  loadSubCategories() {
  
    this.subcategoryService.getDropdownSubCategories().subscribe(subcategories => {
      this.subcategories = this.subcategories.concat(subcategories);
      this.cdr.detectChanges();
    })
  }
}
