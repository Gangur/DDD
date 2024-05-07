import { Component } from '@angular/core';
import PictureUrl from '../../../tools/picturesurlfactory';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  pictureUrl1 = PictureUrl('phone.png');
  pictureUrl2 = PictureUrl('tablet.png');
  pictureUrl3 = PictureUrl('book.png');
}
