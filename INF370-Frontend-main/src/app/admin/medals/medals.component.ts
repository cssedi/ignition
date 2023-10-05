import { AfterViewInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { NgToastService } from 'ng-angular-popup';
import { ChallengeType } from 'src/app/Models/ChallengeType';
import { MedalDto } from 'src/app/Models/medal-dto';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-medals',
  templateUrl: './medals.component.html',
  styleUrls: ['./medals.component.scss']
})
export class MedalsComponent  implements AfterViewInit{
  selectedImage: string | ArrayBuffer | null | undefined;
  medalId : number = 0;
  base64Image: string | null = null;
  myForm: FormGroup;
  updateForm: FormGroup;
  formSubmitted:boolean = false
  name: string = '';
  showModal : boolean = false
  editModal : boolean = false 
  deleteModal : boolean = false 
  challengeTypes: ChallengeType[] = []
   $modalElement!: HTMLElement 
  medals : any[] = []

  editMedal : any
  selectedChallengeType = {id:0, name:''}
   $editModalElement = document.getElementById('edit-modal');
    options : ModalOptions = {
    
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
  };
   constructor(private formBuilder: FormBuilder, private adminService : AdminService, private toast:NgToastService) {
    this.myForm = this.formBuilder.group({
      name: ['', Validators.required],
      challengeTypeId: ['', Validators.required]
    });

    this.updateForm = this.formBuilder.group({
      name: ['', Validators.required],
      challengeTypeId: ['', Validators.required]
    });
   }
  ngAfterViewInit(){
    this.getAllMedal()
    this.$modalElement = document.querySelector('#modalEl')!;
    this.$editModalElement = document.getElementById('edit-modal');
    const modalOptions: ModalOptions = {
    placement: 'center',
    backdrop: 'dynamic',
    backdropClasses: 'bg-gray-900 bg-opacity-50 dark:bg-opacity-80 fixed inset-0 z-40',
    closable: true,
    
  }


   //modal.show();

  }
  viewDeleteModal(medalId : number ){
    this.deleteModal = true
    this.editMedal = this.medals.find( x => x.medalId == medalId)

    this.name = this.medals.find( x => x.medalId == medalId).medalName
  }

  closeDeletModal(){
    this.deleteModal = false 
  }
  deleteMedal( ){
   
    this.adminService.deleteMedal(this.editMedal.medalId).subscribe({
      next: (value) => {
        this.getAllMedal()
        this.toast.success({detail:"SUCCESS", summary:"Medal deleted successfully", duration:5000})
      }, complete: () => {
        this.deleteModal = false 
      },  
      error : (error) => {
      console.log('error on delete', error.error)
      }
    })
  }
  toggleModal() {
    this.showModal = !this.showModal;  
    this.getAllChallengeTypes()
    this.myForm.reset()
    this.base64Image = null
    this.name = ''
  }
  toggleEditModal(medalId? : number) {
    this.editModal = !this.editModal; 
    console.log(medalId)
    //get medal by id
    this.adminService.getMedalById(medalId!).subscribe({
      next:(response)=>{
        console.log(response)
        //get challenge type by id
        this.adminService.getChallengeTypeById(response.challengeTypeId).subscribe({
          next:(response)=>{
            this.selectedChallengeType.id = response.challengeTypeID
            this.selectedChallengeType.name = response.name
            console.log(this.selectedChallengeType)

          }

        })
      },
      error:(error)=>{

      }

    })
    if(medalId != 0 || medalId != null ){
      this.editMedal = this.medals.find( x => x.medalId == medalId)
      this.name = this.editMedal.medalName
      this.base64Image = this.editMedal.imageString
      this.medalId = this.editMedal.medalId
      this.updateForm.controls['challengeTypeId'].setValue(this.editMedal.challengeType.name);
      this.getAllChallengeTypes()

    }
  }
  getAllMedal(){
    this.adminService.getAllMedal().subscribe({
      next: (value) => {
        this.medals = value
        console.log('all medals', value)
      }, 
      error : (error) => {
      console.log('error on create', error.error)
      }
    })
  }
  //CRUD Medal start
  createMedal(){
    this.formSubmitted = true
    var medal : MedalDto ={
    medalName : this.name,
    imageString : this.base64Image!,
    challengeTypeId : parseInt(this.myForm.value.challengeTypeId),
    challengeType: {challengeTypeID: 0,name: '',challenges: []}


    }
    if(this.base64Image){
      if(this.myForm.valid){
        this.adminService.createMedal(medal).subscribe({
          next: (value) => {
          this.toast.success({detail:"SUCCESS", summary:"Medal created successfully", duration:5000})      
        }, 
          error : (error) => {
          console.log('error on create', error.error)
          }, complete: () => {
            this.showModal = false
            this.getAllMedal()
          }
        })
      }

  }else{
    this.toast.error({detail:"ERROR", summary:"Please upload an image", duration:5000})
  }
  }


  EditMedal(){
    var medal : MedalDto ={
      medalName: this.name,
      imageString: this.base64Image!,
      challengeTypeId: parseInt(this.updateForm.value.challengeTypeId),
      challengeType: {challengeTypeID: 0,name: '',challenges: []}
    }

    if(this.base64Image){
      if(this.updateForm.valid){
        this.adminService.EditMedel(this.medalId, medal).subscribe({
          next: (value) => {
            this.toast.success({detail:"SUCCESS", summary:"Medal updated successfully", duration:5000})
            console.log('medal updated', value)
          }, complete: () => {
            const modal = new Modal(this.$editModalElement, this.options);
            this.getAllMedal()
            this.toggleEditModal()
          },
          error : (error) => {
          console.log('error on create', error.error)
          }
        })
      }

  }else{
    this.toast.error({detail:"ERROR", summary:"Please upload an image", duration:5000})
  }


    


  }

  getAllChallengeTypes(){
    this.adminService.getAllChallengeTypes().subscribe(
      {
        next:(response)=>{
          this.challengeTypes = response
          console.log(this.challengeTypes)
        }
      }
    )
  }
  //CRUD Medal end

  //Images
  onImageDrop(event: DragEvent) {
    event.preventDefault();
    this.processImage(event.dataTransfer?.files!);
  }
  handleFileInput(event: any): void {
    const file = event.target.files[0];
  
    // File type validation
    const allowedFileTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/svg+xml'];
    if (!allowedFileTypes.includes(file.type)) {
      // Invalid file type, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'Invalid file type. Please upload an image (JPEG, PNG, GIF, or SVG).' , duration:5000})
      return;
    }
  
    // File size validation (example: 2 MB limit)
    const maxSizeInBytes = 2 * 1024 * 1024; // 2 MB in bytes
    if (file.size > maxSizeInBytes) {
      // File size exceeds the limit, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'File size exceeds the allowed limit (2 MB).' , duration:5000})
      return;
    }
  
    const reader = new FileReader();
  
    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image);
    };
  
    reader.readAsDataURL(file);
  }
  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }

  processImage(files: FileList | null) {
    if (files && files.length > 0) {
      const reader = new FileReader();
      reader.onload = () => {
        this.selectedImage = reader.result;
        this.base64Image = reader.result as string;
      };
      reader.readAsDataURL(files[0]);
    }
  }
  
  clearImageUpload(){
     this.base64Image = null
  }

}
