import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Client, OrderDto } from '../api/http-client';
import { BasketTotals } from '../../models/basket-totals';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private basketSource = new BehaviorSubject<OrderDto | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private _client: Client) { }

  customerIdKey: string = 'customer_id';

  getBasket() {
    let customerId = localStorage.getItem(this.customerIdKey)
    if (customerId) {
      return this._client.v1OrdersByCustomer(customerId).subscribe({
        next: order => {
          this.basketSource.next(order);
          this.calculateTotals();
        },
        error: error => this.createBasket()
      });
    }
    else {
      return this._client.v1CustomeresCreate().subscribe({
        next: customerId => {
          localStorage.setItem(this.customerIdKey, customerId);
          this.createBasket();
        },
      });
    }
  }

  addLineItem(productId: string, quantity = 1) {
    let orderId = this.getBasketValue()?.id;
    if (!orderId || orderId === undefined) {
      this.getBasket();
    }

    return this._client.v1OrdersAddLineItem(orderId!, productId, quantity).subscribe({
      next: order => {
        this.basketSource.next(order);
        this.calculateTotals();
      }
    });
  }

  removeLineItem(productId: string, quantity = 1) {
    let orderId = this.getBasketValue()?.id;
    if (!orderId) {
      this.getBasket();
    }

    return this._client.v1OrdersRemoveLineItem(orderId!, productId, quantity).subscribe({
      next: order => {
        this.basketSource.next(order);
        this.calculateTotals();
      }
    });
  }

  getBasketValue() {
    return this.basketSource.value;
  }

  createBasket() : any{
    let customerId = localStorage.getItem(this.customerIdKey);

    if (customerId) {
      return this._client.v1OrdersCreate(customerId).subscribe({
        next: orderId => this.getBasket(),
      })
    }
    else {
      return this._client.v1CustomeresCreate().subscribe({
        next: customerId => {
          localStorage.setItem(this.customerIdKey, customerId);
          this._client.v1OrdersCreate(customerId).subscribe({
            next: orderId => this.getBasket(),
          })
        },
      });
    }
  }

  private calculateTotals() {
    const basket = this.getBasketValue();
    if (!basket) return;
    const shipping = 0;
    const subtotal = basket.lineItems!.reduce((previous, current) =>
      ((current.priceCurrency === 'EUR' ? 1.1 : 1) * current.priceAmount * current.quantity) + previous, 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({ shipping, total, subtotal });
  }
}
