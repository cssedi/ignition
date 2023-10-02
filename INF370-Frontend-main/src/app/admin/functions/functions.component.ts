import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { OrganisationalFunction } from 'src/app/Models/OrganisationalFunction';
import { Router } from '@angular/router';
import { UnnassignedArchitectVM } from 'src/app/Models/UnassignedArchitectVM';
import { FunctionVM } from 'src/app/Models/FunctionVM';
import * as jspdf from 'jspdf';
import jsPDF from "jspdf";
import { saveAs } from 'file-saver';
@Component({
  selector: 'app-functions',
  templateUrl: './functions.component.html',
  styleUrls: ['./functions.component.scss']
})
export class FunctionsComponent {

  modalHeading: string = "Add Function "
  //arrays
  functionArray: OrganisationalFunction[]=[]
  superArchitects: UnnassignedArchitectVM[]=[]
  functions!:any[]
  //modals
  addFunctionModal: boolean = false;
  updateFunctionModal: boolean = false;
  deleteFunctionModal: boolean = false;
  modalVisible: boolean = false
 //form groups
  updateFunctionForm!: FormGroup;
  addFunctionForm!: FormGroup;
  formSubmitted:boolean = false
  searchTerm!: string
  selectedFunction: any = { name: '', username: 0 }
  selectedArchitect: any = { name: '', username: '' }
  //objects
  functionDetails:OrganisationalFunction={
    functionId: 0,
    superArchitectId: '',
    functionCode: '',
    functionName: '',
    departments: [],
    superArchitectName: '',
    superArchitect: {id: '',awardsArchitectFullName: ''}}

functionViewModel:FunctionVM ={
  FunctionCode: '',
  FunctionName: '',
  SuperArchitectId: ''
}
  constructor(private adminService: AdminService, private fb: FormBuilder, private router: Router,private toast:NgToastService) {}
 
  //Reports begin
  fetchTableData() {
   
    this.adminService.getAllFunction().subscribe(data => {
      this.functions = data
      console.log('all depts', this.functions)
    })
    this.downloadPDF();
  }
 
  downloadPDF() {
    const doc = new jsPDF();
    let yPos = 20;
 
    // Load the logo image
   const logoUrl = 'https://media.licdn.com/dms/image/C4E0BAQG8lNSut2H2_g/company-logo_200_200/0/1648744583668?e=2147483647&v=beta&t=CT7YiTeKyZNhn6D7T26KNqIWwWV8X48u-aF37Qbb-xk'; // Replace with your actual logo URL
 
   // Load the logo image asynchronously
   const img = new Image();
   img.src = logoUrl;// Set up an event listener to ensure the image is loaded before rendering the PDF
   
     // Draw the logo image on the PDF
     doc.addImage(img, 'PNG', 10, 5, 30, 20)
 
 
  
    //  heading with date
    const today = new Date();
    const formattedDate = today.toDateString();
    doc.setFontSize(18);
    doc.text('Function Report- ' + formattedDate, 50, 15);
    yPos += 10;
  
    // Table styles
    const tableHeaders = ['functionCode', 'functionName'];
    const tableHeader = ['Function Code', 'Name'];
    const colWidths = [50, 20, 100]; // Adjust colWidths as needed
    doc.setFontSize(12);
  
    //  headers
    doc.setFillColor(51, 122, 183); // Header background color
    doc.setTextColor(255); // Header text color
    doc.setFont('bold');
    doc.rect(10, yPos, colWidths.reduce((a, b) => a + b), 10, 'F');
    let xPos = 10;
    for (let i = 0; i < tableHeader.length; i++) {
      doc.text(tableHeader[i], xPos + 2, yPos + 8);
      xPos += colWidths[i];
    }
    yPos += 10;
  
    // Draw table data
    doc.setFont('normal');
    this.functions.forEach(course => {
      let xDataPos = 10;
      for (let i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0); // Set text color to black
        doc.text(course[tableHeaders[i]].toString(), xDataPos + 2, yPos + 8);
        xDataPos += colWidths[i];
      }
      yPos += 10;
    });
  
    doc.save('Function_Report.pdf');
  }
 
  //reports end

  exportDataToJson(data: any, fileName: string): void {
    const jsonData = JSON.stringify(data, null, 2); // Convert data to JSON format with indentation
    const blob = new Blob([jsonData], { type: 'application/json' });
    saveAs(blob, fileName + '.json');
  }
  
  // Example usage
  exportButtonClick(): void {
    const today = new Date();
   const formattedDate = today.toDateString();
    const dataToExport = this.functionArray;
    this.exportDataToJson(dataToExport, 'Exported Fuctions - '+ formattedDate);
  }

  ngOnInit(): void {
   //get all functions on page load
    this.getAllFunctions()
    // Form controls and validation
    this.updateFunctionForm = this.fb.group({
      name: new FormControl('', Validators.required),
      functionCode: new FormControl('', Validators.required),
      superArchitectId: new FormControl('', Validators.required),
 
    })
 
    this.addFunctionForm = this.fb.group({
 
     name: new FormControl('', Validators.required),
     functionCode: new FormControl('', Validators.required),
     superArchitectId: new FormControl('', Validators.required),
    })
 
    this.getAllFunctions()
  }
  //Function modals start
  toggleAddFunctionModal() {
   this.addFunctionModal = !this.addFunctionModal;
   this.modalVisible = !this.modalVisible;
   //reset form when modal is closed
   this.addFunctionForm.reset()
   this.getSuperArchitects()
 }
  toggleUpdateFunctionModal() {
   this.updateFunctionModal = !this.updateFunctionModal;
   //reset the update form when the modal is closed
 
     this.updateFunctionForm.reset()
   
 }
  toggleDeleteDepartmentModal() {
   this.deleteFunctionModal = !this.deleteFunctionModal;
 
 }
  //search start
  searchName() {
    this.functionArray = this.functionArray.filter(user =>
      user.functionName.toLowerCase().includes(this.searchTerm.toLowerCase())||
      user.functionCode.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    if(this.searchTerm == ''){
      this.adminService.getAllFunction().subscribe(data => {
        this.functionArray = data
        console.log(data)
      })
    }
  }
  clearSearch() {
   this.searchTerm = ''
 }
   //search end
 
    //CRUD  department functions start
  deleteFunction(){
   this.adminService.deleteFunction(this.functionDetails.functionId).subscribe({
     next: (response) => {
       this.toast.success({detail:"SUCCESS", summary:"Function deleted successfully", duration:5000})
 
     },
     complete: () => {
       this.getAllDepartments()
       this.deleteFunctionModal = false
     },
     error:(error)=>{
      this.toast.error({detail:"ERROR", summary:error.error.message, duration:5000})

      }
     
   })
  }
  updateDepartment() {
   if (!this.updateFunctionForm.invalid) {
 
     this.functionDetails.functionName =this.updateFunctionForm.value.name
     this.functionDetails.functionCode =this.updateFunctionForm.value.functionCode
     this.functionDetails.superArchitectId = this.updateFunctionForm.value.superArchitectId
 
 
     console.log('updated challenge type ', this.functionDetails)
     this.adminService.updateFunction(this.functionDetails.functionId, this.functionDetails).subscribe({
       next: (response) => {
         console.log('updaed ChallengeType - response ', response)
         this.toast.success({detail:"SUCCESS", summary:"Function updated successfully", duration:5000})
       },
       complete: () => {
         this.getAllDepartments()
         this.updateFunctionModal = false
       },
       error:(error)=>{
         this.toast.error({detail:"ERROR", summary:error.message, duration:5000})
       }
     })
   }
  }
 
  getAllDepartments() {
   this.adminService.getAllFunction().subscribe({
     next: (response) => {
       console.log('getAllChallengeTypes', response)
       this.functionArray = response
     },
     complete: () => {
     },
     error: () => {
       //this.challengeTypes = []
     }
   })
 }
 
 createFunction() {
   this.formSubmitted = true
   console.log('Form values:', this.addFunctionForm.value);
   this.functionViewModel.FunctionName =this.addFunctionForm.value.name
   this.functionViewModel.FunctionCode =this.addFunctionForm.value.functionCode
   this.functionViewModel.SuperArchitectId = this.addFunctionForm.value.superArchitectId
   if (!this.addFunctionForm.invalid) {
    

     this.adminService.addFunction(this.functionViewModel).subscribe({
       next: (response) => {
         console.log(response)
         this.toast.success({detail:"SUCCESS", summary:"Function created successfully", duration:5000})
       },
       complete: () => {
         this.getAllDepartments()
         this.addFunctionModal = false
       },
       error:(error)=>{
         this.toast.error({detail:"ERROR", summary:error.message, duration:5000})
 
       }
     })
   }
 }
 
 viewDelete(id: number) {
   this.adminService.getFuntionById(id).subscribe({
     next: (response) => {
      this.functionDetails.functionId = id

      this.functionDetails.functionName =response.functionName
      this.functionDetails.functionCode =response.functionCode
      this.functionDetails.superArchitectId = response.superArchitectId
 
       this.deleteFunctionModal = true
     }
   })
 }
 
 getFunctionById(id: number) {
   this.adminService.getFuntionById(id).subscribe({
     next: (reponse) => {
       this.functionDetails.functionId = id
       this.updateFunctionForm.controls['name'].setValue(reponse.functionName)
       this.updateFunctionForm.controls['functionCode'].setValue(reponse.functionCode)
       this.selectedFunction.username = reponse.functionId
       //catch awards architect null error
 
       
       if(reponse.superArchitectId != null){
              this.updateFunctionForm.controls['superArchitectId'].setValue(reponse.superArchitect.id)
              this.selectedArchitect.name = reponse.superArchitectName
              console.log(this.selectedArchitect.name)
       }
       else{
         this.updateFunctionForm.controls['superArchitectId'].setValue("")
         this.selectedArchitect.name = ""
       }
 
       this.updateFunctionModal = true
       this.getSuperArchitects()
     }
   })
 }
 
 getAllFunctions(){
     this.adminService.getAllFunction()
     .subscribe({
       next:(response)=>{
           this.functionArray = response
    }})
 }
 
 getSuperArchitects(){
  this.adminService.GetUnAssignedSuperArchitects()
  .subscribe({
    next:(response)=>{
      console.log(response)
      this.superArchitects = response
      
    }
  })

 }
     //CRUD department end
 
 }
 