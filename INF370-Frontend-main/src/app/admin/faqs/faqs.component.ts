import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FAQ } from 'src/app/Models/FAQ ';
import { FaqService } from 'src/app/services/faq.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-faqs',
  templateUrl: './faqs.component.html',
  styleUrls: ['./faqs.component.scss']
})
export class FAQsComponent implements OnInit, AfterViewInit {
  faqForm: FormGroup
  faqs: any[] = [];
  newFaq!: FAQ
  editingFaq: FAQ | null = null;
  
  // Modal 
  $createModalElement!: HTMLElement;
  $updateModalElement!: HTMLElement;
  $deleteModalElement!: HTMLElement;
  deleteToast: boolean = false
  modalOptions!: ModalOptions
  createModal!: ModalInterface
  updateModal!: ModalInterface
  deleteModal!: ModalInterface

  constructor(private faqService: FaqService, private fb: FormBuilder) {
    this.faqForm = this.fb.group({
      Question: ['', Validators.required],
      Answer: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadFAQs();
  }

  showCreateModal() {
    this.createModal.show()
  }

  loadFAQs() {
    this.faqService.getFAQ().subscribe({
      next: (faqs) => {
        this.faqs = faqs;
    }
    ,error: (error) => {}
    }
    );
  }

  closeCreateModal() {
    this.createModal.hide()
  }

  createFaq() {

    if (this.faqForm.valid) {
      this.newFaq = { ...this.faqForm.value };
      console.log(this.newFaq)
      this.faqService.createFAQ(this.newFaq).subscribe(
        () => {
          this.loadFAQs();
          this.faqForm.reset();
        },
        (error) => {
          console.log('Error creating FAQ:', error);
        }
      );
    }

  }

  editFaq(faq: FAQ) {
    this.editingFaq = { ...faq };
  }

  updateFaq() {
    if (this.editingFaq) {
      this.faqService.updateFAQ(this.editingFaq.FAQId, this.editingFaq).subscribe(
        () => {
          const index = this.faqs.findIndex(f => f.FAQId === this.editingFaq!.FAQId);
          if (index !== -1) {
            //  this.faqs[index] = { ...this.editingFaq };
            this.editingFaq = null;
          }
        },
        (error) => {
          console.log('Error updating FAQ:', error);
        }
      );
    }
  }

  cancelEdit() {
    this.editingFaq = null;
  }

  deleteFaq() {
    this.faqService.deleteFAQ(this.editingFaq!.FAQId).subscribe({
      next: (value) => {
        console.log(' deleting FAQ:', value);
      }, complete: () => {
        this.deleteModal.hide()
        this.deleteToast = true
        setTimeout(() => {
          this.deleteToast = false;

        }, 2000);
      },
      error: (error) => {
        console.log('Error deleting FAQ:', error);
      }
    });
  }

  showUpdateModal(id: number) {
    console.log(id)
    this.editingFaq = this.faqs.find(f => f.faqId === id);
    console.log(this.editingFaq)
    this.faqForm.patchValue({

      Question: this.faqs.find(f => f.faqId === id).question,
      Answer: this.faqs.find(f => f.faqId === id).answer
    })


    this.updateModal.show()

  }

  hideUpdateModal() {
    this.updateModal.hide()
  }

  hideDeleteModal() {
    this.deleteModal.hide()
  }

  showDeleteModal(id: number) {
    console.log(id)
    this.editingFaq!.FAQId = this.faqs.find(f => f.faqId === id).faqId;

    this.editingFaq!.Question = this.faqs.find(f => f.faqId === id).question;
    console.log(this.editingFaq)
    this.deleteModal.show()
  }

  ngAfterViewInit(): void {
    this.$createModalElement = document.querySelector('#createModal')!;
    this.$updateModalElement = document.querySelector('#updateModal')!;
    this.$deleteModalElement = document.querySelector('#deleteModal')!;

    this.createModal = new Modal(this.$createModalElement, this.modalOptions);
    this.updateModal = new Modal(this.$updateModalElement, this.modalOptions);
    this.deleteModal = new Modal(this.$deleteModalElement, this.modalOptions);

    this.modalOptions = {
      placement: 'center',
      backdrop: 'dynamic',
      backdropClasses: 'bg-gray-900 bg-opacity-50 dark:bg-opacity-80 fixed inset-0 z-40',
      closable: true,
      onHide: () => {
        console.log('modal is hidden');
      },
      onShow: () => {
        console.log('modal is shown');
      },
      onToggle: () => {
        console.log('modal has been toggled');
      }
    }
  }

}