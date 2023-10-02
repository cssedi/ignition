import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { CreateUserDto } from 'src/app/Models/createUserDto';
import { NgToastService } from 'ng-angular-popup';
import { co } from '@fullcalendar/core/internal-common';
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, AfterViewInit {
  createUserDto : CreateUserDto = {
    name : '',
    surname : '',
    email : '',
    userName : '',
    role:'',
  }
  updateModal!: ModalInterface 
  $updateModalElement!: HTMLElement ;
  users! : any[]
  selectRole : string = ''
  modalOptions!: ModalOptions
  updateUser! : any
  searchTerm! : string
  formSubmitted: boolean=false;
  showAddModal : boolean = false
  showUpdateModal : boolean = false
  functions! : any[]
  departments! : any[]
  createForm !: FormGroup;
  updateForm !: FormGroup;
  selectedImage: string | ArrayBuffer | null | undefined;
  base64Image: string | null = null;
  userForm: FormGroup;
  ifIsLoading: boolean = false
  constructor(private adminService : AdminService, private fb : FormBuilder, private toast:NgToastService){
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required, Validators.pattern(/^[^&\/\\#, +()$~%.':"*?<>{}^_]+$/)]],      
      role: ['', Validators.required],
    });
  }
  createUser(){
    this.formSubmitted = true;

    // initialise userDto
    this.createUserDto = this.userForm.value

    if(this.userForm.valid){
      this.ifIsLoading = true;
      this.adminService.createUser(this.createUserDto).subscribe({
        next:(value)=>{
          console.log(value)
          this.getAllUsers()
          this.updateModal.hide()
          this.toast.success({detail: "SUCCESS", summary:value.message + " successfully", duration:5000})
          this.showAddModal = false
           
        }, complete : () => {
          this.ifIsLoading = false
          this.userForm.reset()
          this.formSubmitted=false;

          
        }, error :(err) => {
          console.log(err)
          this.toast.error({detail: "ERROR", summary:err.error.message, duration:5000})
          //set to false in case error
          this.ifIsLoading = false
  
          
        },

      }
      
      )
    }

  }
  // On initialization
  ngOnInit(): void {    // create the form controls and validations 
    this.createForm = this.fb.group({
      name : new FormControl('', Validators.required),  
      surname : new FormControl('', Validators.required),
      email : new FormControl('', [Validators.required, Validators.email]),
      username : new FormControl('', Validators.required),
      role : new FormControl('', Validators.required),

    })
    // get from API
    this.getAllUsers()
    this.getAllFunctions()
    this.getAllDepartments()
  }
    
  removeRole( id : string, role : string){
    this.updateUser.roles = ['CHALLENGER']

    this.adminService.removeUserRole(id, role).subscribe({
      next:(value)=>{
        console.log(value)
        this.getAllUsers()
        this.updateModal.hide()
        this.toast.warning({detail:"REMOVED", summary: "this users role has been succesfully removed", duration:5000})
        this.showAddModal = false;
      }, complete : () => {
      
        this.viewUser(id)
        this.updateModal.hide()
        setTimeout(function(){ location.reload(); }, 4000);
        
      }, error :(err) => {
        this.toast.error({detail:"ERROR", summary:err.error.message, duration:5000})
        this.updateModal.hide()
        this.getAllUsers()
        
      },
    })
  }
  viewUser(userId : string){
       console.log(userId)
       this.updateUser = this.users.find( x => x.id == userId)
       console.log(this.updateUser)

       this.updateModal.show()
  }

  updateUserRole() {
    this.adminService.assignUserRole(this.updateUser.id, this.selectRole).subscribe({
      next:(value)=>{
        console.log(value)
        this.toast.success({detail:"SUCCESS", summary: "this users role has been succesfully updated", duration:5000})
        this.hideUpdateModal()
      }, complete : () => {
        this.getAllUsers()
      }, error :(err) => {
        console.log(err)
        
      },
    })
  } 
  getAllDepartments()
  {
    this.adminService.getAllDepartments().subscribe(data => {
      this.departments = data
      console.log('departments', data)
    })
  
  }
  // Image processing  
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
  onImageDrop(event: DragEvent) {
    event.preventDefault();
    this.processImage(event.dataTransfer?.files!);
  }
  
  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }
  clearImageUpload(){
    this.base64Image = null
 }
  toggleAddModal(){
    this.showAddModal = !this.showAddModal
  }
  toggleUpdateModal(id? : number){
    this.showUpdateModal = !this.showUpdateModal
  }

 
  searchUser() {
    this.users = this.users.filter(user =>
      user.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );

    if(this.searchTerm == ''){
      this.getAllUsers()
    }
  }
 getAllFunctions(){
  this.adminService.getAllFunction().subscribe(data => {
    this.functions = data
    console.log(data)
  })

 }
  getAllUsers(){
    this.adminService.getAllUsers().subscribe({
      next: (reponse) =>{
        console.log('get all users', reponse)
        this.users = reponse
      }, complete : () =>{

      },
      error : (error) =>{
        this.users =[]
      }
    })
  }
  ngAfterViewInit(): void {
    this.$updateModalElement = document.querySelector('#updateModal')!;

    this.modalOptions  = {
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

  this.updateModal = new Modal(this.$updateModalElement, this.modalOptions);

  }

  hideUpdateModal(){
    this.updateModal.hide()
  }



}
