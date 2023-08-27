import { Component,  Output, EventEmitter } from '@angular/core';
import { Popupbar } from 'src/app/Popupbar';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-popupbar',
  templateUrl: './add-popupbar.component.html',
  styleUrls: ['./add-popupbar.component.css']
})
export class AddPopupbarComponent  {
  @Output() AddPopupbar: EventEmitter<Popupbar> = new EventEmitter();
  name!: string;
  location!: string;
  brewer!: string;
  showAddPopupbar!: boolean;
  subscription!: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddPopupbar = value);
   }


  onSubmit() {
    if (!this.name || !this.location || !this.brewer) {
      alert("Please enter all data!");
      return;
    }

    const newPopupbar = {
      name: this.name,
      location: this.location,
      brewer: this.brewer
    }

    this.AddPopupbar.emit(newPopupbar);

    this.name = '';
    this.location = '';
    this.brewer = '';
  }
}
