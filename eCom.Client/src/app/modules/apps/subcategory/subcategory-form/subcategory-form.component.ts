import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { SubcategoryService } from '../../services/subcategory.service';
import { SubCategory } from '../../models/subcategory';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category';
import { Pagination } from '../../models/pagination';
 

@Component({
  selector: 'app-subcategory-form',
  templateUrl: './subcategory-form.component.html',
  styleUrls: ['./subcategory-form.component.scss']
})
export class SubcategoryFormComponent implements OnInit  {

  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  subCategoryForm: FormGroup;
  subCategoryImage: File;
  subCategoryImageUrl = environment.defaultItemImagePath;
  id: number;
  isEdit: boolean;
  subCategory: SubCategory = new SubCategory();
  selectedCategoryId : number;

  pageNumber = 1;
  pageSize = 10;
  categories: Category[] = [];

 
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0
  };


  constructor(private fb: FormBuilder,
    private subCategoryService: SubcategoryService,
    private categoryService : CategoryService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute) {

    const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => {

        this.isLoading = res

      });
    this.unsubscribe.push(loadingSubscr);
  }

  ngOnInit() {

    this.loadCategories();

    this.route.params.subscribe(params => {

      this.id = params['id'];
      if (this.id) {
        this.isEdit = true;
        this.getSubCategory(this.id);
      }
    });

    this.initForm();

  }


  initForm() {

    

    this.subCategoryForm = this.fb.group({

      code: [''],
      categoryId: ['', Validators.compose([Validators.required])],
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
        ]),
      ],
      image: ''
    });
  }

  submit() { 
    const subCategory = new SubCategory();
    subCategory.name = this.subCategoryForm.get('name')?.value;
    subCategory.description = this.subCategoryForm.get('description')?.value;
    subCategory.categoryId = this.subCategoryForm.get('categoryId')?.value;
    subCategory.image = this.subCategoryImage;

    if (!this.subCategoryImage) {
      subCategory.imageUrl = this.subCategory.imageUrl;
    }
    subCategory.code = this.subCategoryForm.get('code')?.value;

    if (this.isEdit) {
      this.subCategoryService.updateSubCategory(this.id, subCategory).subscribe(res => {
        this.router.navigate(['/manage/subcategories']);
      });
    }
    else {
      this.subCategoryService.createSubCategory(subCategory).subscribe(res => {
        this.router.navigate(['/manage/subcategories']);
      });
    }
  }


  // Method to handle file selection
  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.subCategoryImage = file;

      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.subCategoryImageUrl = e.target.result;
        this.cdr.detectChanges();
      };



    }
  }

  getSubCategory(id: number) {
    this.subCategoryService.getSubCategory(id).subscribe(subCategory => {
  
      this.subCategory = subCategory;  

      this.subCategoryForm.patchValue(this.subCategory);
      this.subCategoryImageUrl = this.subCategory.imageUrl ?? this.subCategoryImageUrl;

      this.cdr.detectChanges();
    });

  }

 
 

    loadCategories() {
      // this.categoryService.getCategories(this.pageSize, this.pageNumber).subscribe(res => {
      //   this.categories = this.categories.concat(res.result);
      //   this.pagination = res.pagination; 
    
      //   this.cdr.detectChanges();
      // });

      this.categoryService.getDropdownCategories().subscribe(categories => {
        this.categories = this.categories.concat(categories);
        this.cdr.detectChanges();
      })
    }

    onScrollToEnd() {
    //   this.pageNumber = this.pageNumber + 1
    //  if(this.pageNumber <= this.pagination.totalPages){
    //    this.loadCategories();
    //  }  
    }

 
}
