import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Category, Client, ProductDtoListResultDto } from 'src/app/api/http-client';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  content: ProductDtoListResultDto = { values: [], total: 0 };
  brands: string[] = [];
  categories: Category[] = [Category.Books, Category.Phones, Category.Tablets];
  sortOptions = [
    { name: 'Alphabetical', value: 'Name', descending: false },
    { name: 'Price: Low to high', value: 'Price', descending: false },
    { name: 'Price: High to low', value: 'Price', descending: true },
  ];

  sortSelected = this.sortOptions[0]
  brandSelected: string | undefined = undefined;
  categorySelected: Category | undefined = undefined;

  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(private client: Client) {

  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands(); 
  }

  getProducts() {

    this.client.v1ProductsList(this.categorySelected,
      this.brandSelected,
      this.searchTerm?.nativeElement.value,
      this.sortSelected.value,
      this.sortSelected.descending, this.pageNumber, this.pageSize)
      .subscribe({
        next: respose => this.content = respose!,
        error: error => console.log(error)
    });
  }

  getBrands() {
    this.client.v1ProductsListBrands().subscribe({
      next: respose => this.brands = respose!,
      error: error => console.log(error)
    });
  }

  onBrandSelected(brand: string) {
    if (this.brandSelected !== brand) {
      this.brandSelected = brand;
    }
    else {
      this.brandSelected = undefined;
    }
    this.pageNumber = 1;
    this.getProducts();
  }

  onCategorySelected(category: Category) {
    if (this.categorySelected !== category) {
      this.categorySelected = category;
    }
    else {
      this.categorySelected = undefined;
    }

    this.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(event: any) {
    this.sortSelected = this.sortOptions.find(so => (so.value + '-' + so.descending) == event.target.value)!;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.getProducts();
    }
  }

  onSearch() {
    this.getProducts();
    this.pageNumber = 1;
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';

    this.sortSelected = this.sortOptions[0];
    this.brandSelected = undefined;
    this.categorySelected = undefined;
    this.pageNumber = 1;

    this.getProducts();
  }
}
