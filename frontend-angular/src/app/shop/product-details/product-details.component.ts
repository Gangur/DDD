import { Component, OnInit } from '@angular/core';
import { Client, ProductDto } from '../../api/http-client';
import { ActivatedRoute } from '@angular/router';
import PictureUrl from '../../../tools/picturesurlfactory';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from '../../basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: ProductDto;
  quentity: number = 0;
  quentityInBasket: number = 0;
  pictureUrl?: string;

  constructor(private _client: Client,
    private _activatedRoute: ActivatedRoute,
    private _bcService: BreadcrumbService,
    private _basketService: BasketService)
  {
    this._bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this._activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this._client.v1Products(id).subscribe({
        next: product => {
          {
            this.product = product;
            this._bcService.set('@productDetails', product.name!);
            this._basketService.basketSource$.pipe(take(1)).subscribe({
              next: basket => {
                const item = basket?.lineItems?.find(li => li.productId === id);
                if (item) {
                  this.quentity = item.quantity;
                  this.quentityInBasket = item.quantity;
                }
              }
            })
          };
          this.pictureUrl = PictureUrl(this.product?.pictureName);
        },
        error: error => console.log(error)
      });
    }
  }

  incrementQuantity() {
    this.quentity++;
  }

  decrementQuantity() {
    this.quentity--;
  }

  updateQuantity() {
    if (this.quentity > this.quentityInBasket) {
      const itemsToAdd = this.quentity - this.quentityInBasket;
      this._basketService.addLineItem(this.product!.id, itemsToAdd);
      this.quentityInBasket += itemsToAdd;
    }
    else {
      const itemsToRemove = this.quentityInBasket - this.quentity;
      this._basketService.removeLineItem(this.product!.id, itemsToRemove);
      this.quentityInBasket -= itemsToRemove;
    }
  }

  get buttonText() {
    return this.quentityInBasket === 0 ? 'Add to basket' : 'Update basket';
  }
}
