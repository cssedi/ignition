import { Component } from '@angular/core';

@Component({
  selector: 'app-reward-architects',
  templateUrl: './reward-architects.component.html',
  styleUrls: ['./reward-architects.component.scss']
})
export class RewardArchitectsComponent {
  showModal : boolean = false
  modalVisible  : boolean = false
  toggleModal() {
    this.showModal = !this.showModal;
    this.modalVisible = !this.modalVisible;
  }
  
}
