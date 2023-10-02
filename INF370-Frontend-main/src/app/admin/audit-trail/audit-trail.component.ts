import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import * as XLSX from 'xlsx';
import { Workbook } from 'exceljs';
import Handsontable from 'handsontable';
import * as fs from 'file-saver';
import { Image } from 'exceljs';


const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Component({
  selector: 'app-audit-trail',
  templateUrl: './audit-trail.component.html',
  styleUrls: ['./audit-trail.component.scss']
})
export class AuditTrailComponent {
  Audits!: any[]
  searchTerm!: string
  columns!: any[];
  
  ExportAudit!:[
    User: string,
    Date: Date,
    Action: String,
    Amount: number,
    Quatity: number

  ]

  constructor(private adminService: AdminService) {

  }

  ngOnInit(): void {
    this.getAllAudits()

   
  }

  searchAudit() {
    this.Audits = this.Audits.filter(Audit =>
      Audit.action.toLowerCase().includes(this.searchTerm.toLowerCase())||
      Audit.userId.toLowerCase().includes(this.searchTerm.toLowerCase())||

      this.formatTimestamp(Audit.timestamp).includes(this.searchTerm.toLowerCase())

    );

    if (this.searchTerm == '') {
      this.searchTerm = ''
      this.getAllAudits()
    }

  }
  
  clearSearch() {
    this.searchTerm = ''
    this.getAllAudits()
  }

  formatTimestamp(timestamp: string | number | Date) {
    const date = new Date(timestamp);
    const day = date.getDate().toString().padStart(2, '0');
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const month = monthNames[date.getMonth()];
    const year = date.getFullYear();
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${day} ${month} ${year} ${hours}:${minutes}`;
  }

  exportAuditsToExcel(): void {
    if (this.Audits && this.Audits.length > 0) {

      const today = new Date();
   const formattedDate = today.toDateString();
    
      this.columns = ['ID', 'Action Date', 'User', 'Action','Amount (Hubcoin)', 'Quantity'];

      this.exportAsExcelFile( 'Audit Trail - '+ formattedDate,'',this.columns,this.Audits,'Audit Trail - ' + formattedDate,'Audit Trail');
    } else {
      // Handle the case where there are no audits to export
      console.log('No audits to export.');
    }
  }
  exportAsExcelFile(
    reportHeading: string,
    reportSubHeading: string,
    headersArray: any[],
    json: any[],
    // footerData: any,
    excelFileName: string,
    sheetName: string
) {
    const header = headersArray;
    const data = json;
    

    /* Create workbook and worksheet */
    const workbook = new Workbook();
    workbook.creator = 'Admin';
    workbook.lastModifiedBy = 'Admin';
    workbook.created = new Date();
    workbook.modified = new Date();
    const worksheet = workbook.addWorksheet(sheetName);

  


    /* Add Header Row */
    worksheet.addRow([]);
    worksheet.mergeCells('A1:' + this.numToAlpha(header.length - 1) + '1');
    worksheet.getCell('A1').value = reportHeading;
    worksheet.getCell('A1').alignment = { horizontal: 'center' };
    worksheet.getCell('A1').font = { size: 15, bold: true };

    if (reportSubHeading !== '') {
        worksheet.addRow([]);
        worksheet.mergeCells('A2:' + this.numToAlpha(header.length - 1) + '2');
        worksheet.getCell('A2').value = reportSubHeading;
        worksheet.getCell('A2').alignment = { horizontal: 'center' };
        worksheet.getCell('A2').font = { size: 12, bold: false };
    }

    worksheet.addRow([]);

    /* Add Header Row */
    const headerRow = worksheet.addRow(header);

    // Cell Style : Fill and Border
    headerRow.eachCell((cell, index) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: '0000ff' },
            bgColor: { argb: '0000ff' }
        };
        cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
        cell.font = { size: 12, bold: true };

        worksheet.getColumn(index).width = header[index - 1].length < 20 ? 20 : header[index - 1].length;
    });

    // Get all columns from JSON
    let columnsArray: any[];
    for (const key in json) {
        if (json.hasOwnProperty(key)) {
            columnsArray = Object.keys(json[key]);
        }
    }

    // Add Data and Conditional Formatting
    data.forEach((element: any) => {

        const eachRow: any[] = [];
        columnsArray.forEach((column) => {
            eachRow.push(element[column]);
        });

        if (element.isDeleted === 'Y') {
            const deletedRow = worksheet.addRow(eachRow);
            deletedRow.eachCell((cell) => {
                cell.font = { name: 'Calibri', family: 4, size: 11, bold: false, strike: true };
            });
        } else {
            worksheet.addRow(eachRow);
        }
    });

    worksheet.addRow([]);

    

    /*Save Excel File*/
    workbook.xlsx.writeBuffer().then((data: ArrayBuffer) => {
        const blob = new Blob([data], { type: EXCEL_TYPE });
        fs.saveAs(blob, excelFileName + EXCEL_EXTENSION);
    });
}

private numToAlpha(num: number) {

    let alpha = '';

    for (; num >= 0; num = parseInt((num / 26).toString(), 10) - 1) {
        alpha = String.fromCharCode(num % 26 + 0x41) + alpha;
    }

    return alpha;
}



 


  
  getAllAudits() {
    this.adminService.getAllAudits().subscribe({
      next: (response) => {
        console.log('getAllChallengeTypes', response)
      
        this.Audits = response
        console.log('depart challenges', response)
        this.Audits.forEach(element => {
          element.timestamp = new Date(element.timestamp).toString();
          
          element.timestamp = element.timestamp.replace(/ GMT\+\d{4} \(.*\)$/, "");
          

          const timestamp = new Date(element.timestamp);
          const day = timestamp.getDate();
          const month = timestamp.toLocaleString('default', { month: 'short' });
          const year = timestamp.getFullYear();
          const hours = timestamp.getHours();
          const minutes = timestamp.getMinutes()
          const formattedMinutes = minutes < 10 ? `0${minutes}` : minutes;


          element.timestamp = `${day} ${month} ${year} ${hours}:${formattedMinutes}`;

          
          this.Audits.push(element)

          
        });
      },
      complete: () => {
      },
      error: () => {
      }
    })
  }
}
