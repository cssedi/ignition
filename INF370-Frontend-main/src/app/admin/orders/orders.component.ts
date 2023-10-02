import { Component, OnInit } from '@angular/core';
import jsPDF from 'jspdf';
import { AdminService } from 'src/app/services/admin.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders : any[] =[]
  constructor (private adminService : AdminService){}
  dateString = '2023-07-29T13:41:10.9824859';


 //export begin 
 exportDataToJson(data: any, fileName: string): void {
  const jsonData = JSON.stringify(data, null, 2); // Convert data to JSON format with indentation
  const blob = new Blob([jsonData], { type: 'application/json' });
  saveAs(blob, fileName + '.json');
}

// Example usage
exportButtonClick(): void {

  const today = new Date();
  const formattedDate = today.toDateString();
  const dataToExport = this.orders;
  this.exportDataToJson(dataToExport, 'Placed Orders - ' + formattedDate);
}

  ngOnInit(): void {
    this.getAllOrders()
    console.log(this.convertToDate(this.dateString))
  }
  
  convertToDate(dateString: string): string {
    const dateParts = dateString.split('T')[0].split('-');
    const year = dateParts[0];
    const month = this.getMonthName(+dateParts[1]);
    const day = dateParts[2];

    return `${month} ${day}, ${year}`;
  }

  getMonthName(monthNumber: number): string {
    const months = [
      'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
      'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
    ];
    return months[monthNumber - 1];
  }
  
  getAllOrders(){
    this.adminService.getAllPrizeOrders().subscribe( { 
      next: (value : any[]) => {

        value.forEach(element => {
          // convert the date 
          const placedDate = new Date(element.datePlaced);
          const day = placedDate.getDate();
          const month = placedDate.toLocaleString('default', { month: 'short' });
          const year = placedDate.getFullYear();
          const hours = placedDate.getHours();
          const minutes = placedDate.getMinutes()
          element.datePlaced =` ${month} ${day}, ${year} ${hours}:${minutes}`
         // element.datePlaced = this.convertToDate(element.datePlaced)
          this.orders.push(element)
        });

      console.log('orders', this.orders)
    },})
  }

  placeSupplierOrder(){
    this.adminService.createSupplierOrder(this.orders).subscribe({
      next: (value) => {
        console.log(value)
      },
      complete: () => {
        // download pdf 
        this.generatePDF();
        
      }, 
      error: (error) => {
        
      }
    })
  }
  

  generatePDF() {
    const doc = new jsPDF();
    let yPos = 20;

    // Heading with date
    const today = new Date();
    const formattedDate = today.toDateString();
    doc.setFontSize(18);
    doc.addFont("Arimo-Regular.ttf", "Arimo", "normal");
    doc.setFont("Arimo");

    doc.text('Supplier Order - ' + formattedDate, 10, yPos);
    yPos += 10;

    // Table styles
    const tableHeaders = ['Prize Name', 'Order Status', 'Date Placed', 'Challenger'];
    const colWidths = [40, 40, 40, 40]; // Adjust colWidths as needed
    doc.setFontSize(12);

    // Headers
    doc.setFillColor(51, 122, 183); // Header background color
    doc.setTextColor(255); // Header text color
    doc.setFont('bold');
    doc.rect(10, yPos, colWidths.reduce((a, b) => a + b, 0), 10, 'F');
    let xPos = 10;
    for (let i = 0; i < tableHeaders.length; i++) {
      doc.addFont("Arimo-Regular.ttf", "Arimo", "normal");
      doc.text(tableHeaders[i], xPos + 2, yPos + 8);
      xPos += colWidths[i];
    }
    yPos += 10;

    // Draw table data
    doc.addFont("Arimo-Regular.ttf", "Arimo", "normal");
   
    doc.setFont("Arimo");

    this.orders.forEach(order => {
      let xDataPos = 10;
      for (let i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0); // Set text color to black
        if (tableHeaders[i] === 'Prize Name') {
          doc.addImage(order.prize.frontImgURL, 'JPEG', xDataPos + 2, yPos, 8, 8);
          xDataPos += colWidths[i];
        }
        if (tableHeaders[i] === 'Order Status') {
          doc.text(order.prizeOrderStatus.status, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        } 

        if (tableHeaders[i] === 'Date Placed') {
          doc.text(order.datePlaced, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        } 

        if (tableHeaders[i] === 'Challenger') {
          doc.text(order.challenger.name, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        } 

        // else {
        //   doc.text('last test', xDataPos + 2, yPos + 8);
        //   xDataPos += colWidths[i];
        // }
      }
      yPos += 10;
    });

    doc.save('Suplier_Order.pdf');
  }

}
