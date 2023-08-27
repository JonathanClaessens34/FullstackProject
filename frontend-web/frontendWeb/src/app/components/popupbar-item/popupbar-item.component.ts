import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Popupbar } from 'src/app/Popupbar';

@Component({
  selector: 'app-popupbar-item',
  templateUrl: './popupbar-item.component.html',
  styleUrls: ['./popupbar-item.component.css']
})
export class PopupbarItemComponent {
  @Input() popupbar!: Popupbar
  @Output() DeletePopupbar: EventEmitter<Popupbar> = new EventEmitter()



  onDelete(popupbar: Popupbar): void {
    alert("Still needs to be implemented");
    //TO DO, call endpoint to delete given popupbar
    this.DeletePopupbar.emit(popupbar);
  }
}
