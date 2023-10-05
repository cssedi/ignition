import {
  Component, OnInit, ViewChild, ElementRef
} from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as jspdf from 'jspdf';
import jsPDF from "jspdf";
import { Department } from 'src/app/Models/Department';
import { OrganisationalFunction } from 'src/app/Models/OrganisationalFunction';
import { NgToastService } from 'ng-angular-popup';
import { UnnassignedArchitectVM } from 'src/app/Models/UnassignedArchitectVM';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.scss']
})
export class DepartmentsComponent {

  modalHeading: string = "Add Function "

  //arrays
  departments!: any[]
  DepartmentArray: Department[] = []
  Functions: OrganisationalFunction[] = []
  awardsArchitects: UnnassignedArchitectVM[] = []
  users!: any[]

  //modals
  addDepartmentModal: boolean = false
  updateDepartmentModal: boolean = false
  deleteDepartmentModal: boolean = false
  depUserModal: boolean = false
  modalVisible: boolean = false

  //form groups
  updateDepartmentForm!: FormGroup
  addDepartmentForm!: FormGroup
  formSubmitted: boolean = false
  searchTerm!: string
  selectedFunction: any = { name: '', username: 0 }
  selectedArchitect: any = { name: '', username: '' }

  //objects
  departmentDetails: Department = {
    departmentId: 0,
    departmentCode: '',
    name: '',
    functionId: 0,
    awardsArchitectId: '',
    checked: null,
    awardsArchitect: undefined
  }
  currentDep: any

  constructor(private adminService: AdminService, private fb: FormBuilder, private router: Router, private toast: NgToastService) { }
//Reports begin
fetchTableData() {
   
  this.adminService.getAllDepartments().subscribe(data => {
    this.departments = data
    console.log('all depts', this.departments)
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
  doc.text('Department Report- ' + formattedDate, 50, 15);
  yPos += 10;

  // Table styles
  const tableHeaders = ['departmentCode', 'name'];
  const tableHeader = ['Department Code', 'Name'];
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
  this.departments.forEach(course => {
    let xDataPos = 10;
    for (let i = 0; i < tableHeaders.length; i++) {
      doc.setTextColor(0); // Set text color to black
      doc.text(course[tableHeaders[i]].toString(), xDataPos + 2, yPos + 8);
      xDataPos += colWidths[i];
    }
    yPos += 10;
  });

  doc.save('Department_Report.pdf');
}

  //export begin 
  exportDataToJson(data: any, fileName: string): void {
    const jsonData = JSON.stringify(data, null, 2); // Convert data to JSON format with indentation
    const blob = new Blob([jsonData], { type: 'application/json' });
    saveAs(blob, fileName + '.json');
  }

  // Example usage
  exportButtonClick(): void {
    const dataToExport = this.DepartmentArray;
    this.exportDataToJson(dataToExport, 'Exported Departments');
  }

  ngOnInit(): void {
    //get all departments on page load
    this.adminService.getAllDepartmentsMain().subscribe({
      next: (response) => {
        this.DepartmentArray = response
        console.log(response)
      }
    })
    // Form controls and validation
    this.updateDepartmentForm = this.fb.group({
      name: new FormControl('', Validators.required),
      departmentCode: new FormControl('', Validators.required),
      awardsArchitectName: new FormControl('', Validators.required),
      functionId: new FormControl('', Validators.required),

    })

    this.addDepartmentForm = this.fb.group({

      name: new FormControl('', Validators.required),
      departmentCode: new FormControl('', Validators.required),
      awardsArchitectName: new FormControl('', Validators.required),
      functionId: new FormControl('', Validators.required),
    })

    this.getAllFunctions()
  }

  //department modals start
  toggleAddDepartmentModal() {
    this.addDepartmentModal = !this.addDepartmentModal;
    this.modalVisible = !this.modalVisible;
    //reset form when modal is closed
    this.addDepartmentForm.reset()
    this.getAwardsArchitects()
  }

  toggleUpdateDepartmentModal() {
    this.updateDepartmentModal = !this.updateDepartmentModal;
    //i want to reset the update form when the modal is closed
    this.updateDepartmentForm.reset()
  }

  toggleDeleteDepartmentModal() {
    this.deleteDepartmentModal = !this.deleteDepartmentModal;
  }

  //search start
  searchName() {
    this.departments = this.departments.filter(user =>
      user.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    if (this.searchTerm == '') {
      this.adminService.getAllDepartments().subscribe(data => {
        this.departments = data
        console.log(data)
      })
    }
  }

  clearSearch() {
    this.searchTerm = ''
  }
  //search end

  //CRUD  department functions start
  deleteDepartment() {
    this.adminService.deleteDepartment(this.departmentDetails.departmentId).subscribe({
      next: (response) => {
        this.toast.success({ detail: "SUCCESS", summary: "Department deleted successfully", duration: 5000 })

      },
      complete: () => {
        this.getAllDepartments()
        this.deleteDepartmentModal = false
      },
      error: (error) => {
        this.toast.error({ detail: "ERROR", summary: error.error.message, duration: 5000 })
      }
    })
  }

  updateDepartment() {
    if (!this.updateDepartmentForm.invalid) {

      this.departmentDetails.name = this.updateDepartmentForm.value.name
      this.departmentDetails.departmentCode = this.updateDepartmentForm.value.departmentCode
      this.departmentDetails.awardsArchitectId = this.updateDepartmentForm.value.awardsArchitectName
      this.departmentDetails.functionId = this.updateDepartmentForm.value.functionId


      console.log('updated challenge type ', this.departmentDetails)
      this.adminService.updateDepartment(this.departmentDetails.departmentId, this.departmentDetails).subscribe({
        next: (response) => {
          console.log('updaed ChallengeType - response ', response)
          this.toast.success({ detail: "SUCCESS", summary: "Department updated successfully", duration: 5000 })
        },
        complete: () => {
          this.getAllDepartments()
          this.updateDepartmentModal = false
        },
        error: (error) => {
          this.toast.error({ detail: "ERROR", summary: error.message, duration: 5000 })
        }
      })
    }
  }

getAllDepartments() {
  this.adminService.getAllDepartmentsMain().subscribe({
    next: (response) => {
      console.log('getAllChallengeTypes', response)
      this.DepartmentArray = response
    },
    complete: () => {
    },
    error: () => {
      //this.challengeTypes = []
    }
  })
}

createChallengeType() {

  this.formSubmitted = true
  if (!this.addDepartmentForm.invalid) {
   
    this.departmentDetails.name =this.addDepartmentForm.value.name
    this.departmentDetails.departmentCode =this.addDepartmentForm.value.departmentCode
    this.departmentDetails.awardsArchitectId = this.addDepartmentForm.value.awardsArchitectName
    this.departmentDetails.functionId = this.addDepartmentForm.value.functionId
    console.log(this.departmentDetails)
    this.adminService.addDepartment(this.departmentDetails).subscribe({
      next: (response) => {
        console.log(response)
        this.toast.success({detail:"SUCCESS", summary:"Department created successfully", duration:5000})
      },
      complete: () => {
        this.getAllDepartments()
        this.addDepartmentModal = false
      },
      error:(error)=>{
        this.toast.error({detail:"ERROR", summary:error.error.message, duration:5000})
        console.log(error)
      }
    })
  }
}


  viewDelete(id: number) {
    this.adminService.getDepartmentById(id).subscribe({
      next: (response) => {
        this.departmentDetails.name = response.name
        this.departmentDetails.departmentId = response.departmentId
        this.departmentDetails.functionId = response.functionId
        this.departmentDetails.departmentCode = response.departmentCode
        this.departmentDetails.awardsArchitectId = response.awardsArchitectName

        this.deleteDepartmentModal = true
      }
    })
  }

  getDepartmentById(id: number) {
    this.adminService.getDepartmentById(id).subscribe({
      next: (reponse) => {

        console.log('getDepartmentId', reponse)
        this.departmentDetails.departmentId = id
        this.updateDepartmentForm.controls['name'].setValue(reponse.name)
        this.updateDepartmentForm.controls['departmentCode'].setValue(reponse.departmentCode)
        this.updateDepartmentForm.controls['functionId'].setValue(reponse.functionId)
        this.selectedFunction.username = reponse.functionId
        //catch awards architect null error


        if (reponse.awardsArchitect != null) {
          this.updateDepartmentForm.controls['awardsArchitectName'].setValue(reponse.awardsArchitect.id)
          this.selectedArchitect.name = reponse.awardsArchitect.name + " " + reponse.awardsArchitect.surname
          console.log(this.selectedArchitect.name)
        }
        else {
          this.updateDepartmentForm.controls['awardsArchitectName'].setValue("")
          this.selectedArchitect.name = ""
        }

        this.updateDepartmentModal = true
        this.selectedFunction.name = reponse.function.functionCode
        this.getAwardsArchitects()
      }
    })
  }

  getAllFunctions() {
    this.adminService.getAllFunction()
      .subscribe({
        next: (response) => {
          this.Functions = response
          console.log(this.Functions)
        }
      })
  }

  getAwardsArchitects() {
    this.adminService.GetUnAssignedAwardsArchitects()
      .subscribe(
        {
          next: (response) => {
            this.awardsArchitects = response
            console.log(this.awardsArchitects)
          }
        }
      )
  }
  //CRUD department end

  // Get the users in specific department
  getDepUsers(code: string) {
    this.currentDep = code

    this.adminService.getAllUsers().subscribe({
      next: (response) => {
        this.users = response
        this.users = this.users.filter(user => user.department == code)
        console.log(this.users)
      }
    })

    this.toggleUserModal()
  }

  toggleUserModal() {
    this.depUserModal = !this.depUserModal;
  }
}
