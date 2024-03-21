import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Subscription, debounceTime } from 'rxjs';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss']
})
export class CategoryFormComponent implements OnInit {
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  categoryForm: FormGroup;
  categoryImage: File;
  categoryImageUrl = environment.defaultItemImagePath;
  id: number;
  isEdit: boolean;
  category: Category;

  constructor(private fb: FormBuilder,
    private categoryService: CategoryService,
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
    this.categoryForm = this.fb.group({
      
      code: [''],
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
    debugger
    const category = new Category();
    category.name = this.categoryForm.get('name')?.value;
    category.description = this.categoryForm.get('description')?.value; 
    category.image = this.categoryImage;
    if (!this.categoryImage) {
      category.imageUrl = this.category.imageUrl;
    } 
    category.code = this.categoryForm.get('code')?.value; 

    if (this.isEdit) {
      this.categoryService.updateCategory(this.id,category).subscribe(res => {
        this.router.navigate(['/manage/categories']);
      });
    }
    else {
      this.categoryService.createCategory(category).subscribe(res => {
        this.router.navigate(['/manage/categories']);
      });
    }
  }


  // Method to handle file selection
  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.categoryImage = file;

      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (e: any) => {
        this.categoryImageUrl = e.target.result;
        this.cdr.detectChanges();
      };



    }
  }

  getCategory(id: number) {
    this.categoryService.getCategory(id).subscribe(category => {
debugger
      this.category = category; 
      this.categoryForm.patchValue(this.category);
      this.categoryImageUrl = this.category.imageUrl ?? this.categoryImageUrl;

      this.cdr.detectChanges();
    });

  }
}
