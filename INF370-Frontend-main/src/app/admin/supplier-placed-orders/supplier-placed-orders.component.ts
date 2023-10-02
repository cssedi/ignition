import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-supplier-placed-orders',
  templateUrl: './supplier-placed-orders.component.html',
  styleUrls: ['./supplier-placed-orders.component.scss']
})
export class SupplierPlacedOrdersComponent implements OnInit {
  supplierOrders : any =[];

  constructor(private adminService: AdminService) {
    

  }

  
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
  const dataToExport = this.supplierOrders;
  this.exportDataToJson(dataToExport, 'Supplier Orders - '+ formattedDate);
}

  ngOnInit(): void {
    this.adminService.getSupplierOrder().subscribe({
      next:(orders: any[])=>{
        orders.forEach(element => {
          const placedDate = new Date(element.datePlaced);
          const day = placedDate.getDate();
          const month = placedDate.toLocaleString('default', { month: 'short' });
          const year = placedDate.getFullYear();
          const hours = placedDate.getHours();
          const minutes = placedDate.getMinutes()
          element.datePlaced =` ${month} ${day}, ${year} ${hours}:${minutes}`
         // element.datePlaced = this.convertToDate(element.datePlaced)
          this.supplierOrders.push(element)
        });
        
        console.log(this.supplierOrders)
      },
      error: (response)=>{
        console.log(response)
      }
    })
  }

  

}
