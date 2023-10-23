import { Component, ElementRef, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { HelpDashService } from '../services/help-dash.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'app-help-page',
  templateUrl: './help-page.component.html',
  styleUrls: ['./help-page.component.scss']
})
export class HelpPageComponent {
  helps: any[] = [];
  origHelps: any[] = []; // Trying to minimize API calls

  locations: any[] = [];
  locationPairs: [number, string][] = [
    [1, ''],
    [22, 'Admin'],
    [23, 'Complete Challenge'],
    [16, 'Create Challenge'],
    [26, 'Create Prize'],
    [11, 'Challenge Types'],
    [15, 'Challenges'],
    [36, 'Challenge Reports'],
    [5, 'Departments'],
    [6, 'Functions'],
    [17, 'Forgot Password'],
    [8, 'Inbox'],
    [2, 'Login'],
    [35, 'My Orders'],
    [32, 'Other Profile'],
    [27, 'Orders'],
    [21, 'User Challenges'],
    [10, 'Users'],
    [31, 'Profile'],
    [3, 'Home'],
    [39, 'Help Dashboard'],
    [18, 'Library'],
    [13, 'Medals'],
    [25, 'Submissions'],
    [38, 'Supplier Orders'],
    [24, 'Reward Shop'],
    [12, 'Reward Category'],
    [33, 'Reward Architect Challenges'],
    [9, 'Reward Architects'],
    [14, 'Reports'],
    [19, 'Register'],
    [20, 'Reset Password'],
    [7, 'Emojis'],
    [37, 'Leaderboard'],
    [28, 'View Rewards'],
    [30, 'View FAQs'],
    [4, 'Social Feed'],
    [34, 'Test'],
  ];
  origLocs: [number, string][] = this.locationPairs; // Trying to minimize API calls

  searchTerm!: string;

  constructor(private location: Location, private router: Router, private helpDashService: HelpDashService, private renderer: Renderer2, private el: ElementRef, private route: ActivatedRoute) {

  }
  ngOnInit() {

    this.getHelps()
    this.locations = this.locationPairs
  }

  ngAfterViewInit() {
    // This is for the scrolling to the relevant help elements
    this.route.queryParams.subscribe(params => {
      const elementId = params['elementId'];
      console.log(elementId);
      const element = document.getElementById(elementId);

      if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start', inline: 'nearest' });
      }
    });
  }
  goBack() {
    this.location.back();
  }
  getHelps() {
    try {
      // Clear this bad boi here
      this.helps = [];

      this.getLocations()

      this.helpDashService.getHelp().subscribe({
        next: (response) => {
          console.log('Help retrieved successfully:', response);
          this.helps = response;
          this.origHelps = response;
        },
        error: (error) => {
          console.error('Error retrieving help:', error);
        },
        complete: () => {
          console.log('Help retrieval completed');
        }
      });
    }

    catch (e) {
      console.log('Error', e);
    }
  }

  getLocations() {
    // Retrieve the locations
    this.helpDashService.getLocations().subscribe({
      next: (response) => {
        this.locations = response;
      },
      error: (error) => {
        console.error('Error retrieving locations:', error);
      },
      complete: () => {
        console.log('Locations retrieval completed');
      }
    })
  }

  // The search algorithm will check the location name, then the help name, and then the actual description of the helps to ensure that the search is comprehensive. Don't know what a user is going to search for exactly.
  // Still a little iffy about the search not updating properly when bakcspacing, but satisficing for now.
  searchHelps() {
    if (this.searchTerm != '') {
      // This part works fine
      this.locationPairs = this.locationPairs.filter(location =>
        location[1].toLowerCase().includes(this.searchTerm.toLowerCase())
      )

      if (this.locationPairs.length == 0) {
        this.helps = this.origHelps
        this.locationPairs = this.origLocs

        console.log("Second step!!!")

        // Filter the locations as well, bc now all the locations are still displaying is the thing.
        this.helps = this.helps.filter(help =>
          help.name.toLowerCase().includes(this.searchTerm.toLowerCase())
        );
        
        // For each help left in the help array, find the corresponding locations
        this.helps.forEach(help => {
          this.locationPairs = this.locationPairs.filter(location => location[0] === help.location.locationId);
        });

        // Then check help descriptions
        if (this.helps.length == 0) {
          this.locationPairs = this.origLocs
          this.helps = this.origHelps

          this.helps = this.helps.filter(help =>
            help.description.toLowerCase().includes(this.searchTerm.toLowerCase())
          );

          // For each help left in the help array, find the corresponding locations
          this.helps.forEach(help => {
            this.locationPairs = this.locationPairs.filter(location => location[0] === help.location.locationId);
          });
        }
      }
    }
    else
    {
      this.clearSearch()
    }
  }

  clearSearch() {
    this.searchTerm = ''

    //Again, trying to avoid calling the API uneccesarily
    this.helps = this.origHelps
    this.locationPairs = this.origLocs
    // this.getHelps()
  }

  // Technically not necessary
  // downloadHelpPDF() {
  //   const doc = new jsPDF();

  //   const currentDate = new Date();
  //   const day = String(currentDate.getDate()).padStart(2, '0');
  //   const month = String(currentDate.getMonth() + 1).padStart(2, '0');
  //   const year = String(currentDate.getFullYear());
  //   const formattedDate = day + month + year;

  //   const logoSrc = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQa8wwT3d7Q-UCENieoWH3frKQ8-XkQMy6r1utPjjIIoQ&s';
  //   const title = 'Ignition Help Document';
  //   doc.addImage(logoSrc, 'PNG', 80, 20, 55, 30);
  //   doc.setFontSize(18);
  //   doc.text(title, 105, 60, { align: 'center' });

  //   const description = 'Help document for the ignition system, as at ' + formattedDate;
  //   doc.setFontSize(10);
  //   doc.setTextColor(100);
  //   doc.text(description, 20, 70, { align: 'justify' });
  //   const maxPageHeight = doc.internal.pageSize.height - 20;

  //   let yPos = 65;

  //   // const headers = ['Position', 'Challenger', 'Department', 'Completion Rate'];
  //   // No headers, going to replicate the cards like I did.

  //   this.topOverallChallengers.forEach((challenger, index) => {
  //     const remainingPageSpace = maxPageHeight - yPos;
  //     const itemHeight = 20;

  //     if (remainingPageSpace < itemHeight) {
  //       doc.addPage();
  //       yPos = 20;
  //     }

  //     let xPos = 20;

  //     headers.forEach((header) => {
  //       // Set the font size and style
  //       doc.setFontSize(12);
  //       doc.setFont('calibri', 'normal');

  //       let cellValue;
  //       if (header === 'Challenger') {
  //         cellValue = challenger[0].name;
  //       } else if (header === 'Department') {
  //         cellValue = challenger[0].department;
  //       } else if (header === 'Completion Rate') {
  //         cellValue = challenger[1];
  //       }

  //       // Draw the cell with aligned text
  //       doc.text(cellValue, xPos, yPos, { align: 'center' });

  //       // Increase the x position for the next cell
  //       xPos += 50; // Adjust the value as needed for column width
  //     });

  //     // Increase the y position for the next row
  //     yPos += 20;
  //   });

  //   doc.save('OverallLeaderboard' + formattedDate + '.pdf');
  // }

}
