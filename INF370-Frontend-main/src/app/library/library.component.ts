import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Popover } from "flowbite";
import type { PopoverOptions, PopoverInterface } from "flowbite";

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss']
})
export class LibraryComponent {
  checkboxForm!: FormGroup;

  checkboxValue: boolean = false; // Set the initial value as required
  isCheckboxDisabled(checkboxName: string): boolean {
    const checkedCount = Object.values(this.checkboxForm.value).filter(value => value).length;
    return true;
  }
 constructor(private formBuilder: FormBuilder){
  this.checkboxForm = this.formBuilder.group({
    checkbox1: false,
    checkbox2: false,
    checkbox3: false
  });

  
  // set the popover content element
const $targetEl: HTMLElement = document.getElementById('popoverContent')!;

// set the element that trigger the popover using hover or click
const $triggerEl: HTMLElement = document.getElementById('popoverButton')!;

// options with default values
const options: PopoverOptions = {
  placement: 'top',
  triggerType: 'hover',
  offset: 10,
  onHide: () => {
      console.log('popover is shown');
  },
  onShow: () => {
      console.log('popover is hidden');
  },
  onToggle: () => {
      console.log('popover is toggled');
  }
};

if ($targetEl) {
    /*
    * targetEl: required
    * triggerEl: required
    * options: optional
    */
    const popover: PopoverInterface = new Popover($targetEl, $triggerEl, options);

    popover.show();
}
 }
}
