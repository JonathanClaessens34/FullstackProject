import {Component, OnInit} from '@angular/core';
import { Popupbar } from '../Popupbar';
import { PopupbarService } from '../services/popupbar.service';
import { UiService } from '../services/ui.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-popupbar-add',
  templateUrl: './popupbar-add.component.html',
  styleUrls: ['./popupbar-add.component.css']
})
export class PopupbarAddComponent implements OnInit{
  popupbars: Popupbar[] = [];
  showAddPopupbar!: boolean;
  subscription!: Subscription;

  constructor(private popupbarService: PopupbarService, private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddPopupbar = value);
  }

  ngOnInit(): void {
    this.popupbarService.getPopupbars().subscribe((popupbars) =>
      this.popupbars = popupbars);
  }

  addPopupbar(popupbar: Popupbar) {
    this.popupbarService.addPopupbar(popupbar).subscribe((popupbar) =>
      this.popupbars.push(popupbar));
  }

  toggleAddPopupbar() {
    this.uiService.toggleAddPopupbar();
  }
}
