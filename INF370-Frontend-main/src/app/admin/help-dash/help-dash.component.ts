import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Help } from 'src/app/Models/Help';
import { HelpDashService } from 'src/app/services/help-dash.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import * as saveAs from 'file-saver';
import jsPDF from 'jspdf';

// Consider changing these --------------------------//
@Component({
    selector: 'app-faqs',
    templateUrl: './help-dash.component.html',
    styleUrls: ['./help-dash.component.scss']
})

export class HelpDashComponent implements OnInit, AfterViewInit {
    helpForm: FormGroup
    helpList: any[] = [];
    locList: any[] = [];
    newHelp: any;
    editingHelp: any;

    // Perhaps show this in the dropdown instead of the raw location names?
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

    searchTerm!: string

    // Modal 
    $createModalElement!: HTMLElement;
    $updateModalElement!: HTMLElement;
    $deleteModalElement!: HTMLElement;
    deleteToast: boolean = false
    modalOptions!: ModalOptions
    createModal!: ModalInterface
    updateModal!: ModalInterface
    deleteModal!: ModalInterface

    constructor(private helpDashService: HelpDashService, private fb: FormBuilder, private toastService: NgToastService) {
        this.helpForm = this.fb.group({
            Name: ['', Validators.required],
            Description: ['', Validators.required],
            Location: ['', Validators.required]
        });
    }

    ngOnInit() {
        this.GetHelp();
        this.GetLocations()
    }

    ngAfterViewInit() {
        // Modals instantiation
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

            // Remove these console logs when finished debugging.
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

    GetHelp() {
        this.helpDashService.getHelp().subscribe(
            (help) => {
                this.helpList = help;
                console.log("This is the helplsit array: ", this.helpList)
            },
            (error) => {
                console.log('Error fetching help info:', error);
            }
        );
    }

    GetLocations() {
        this.helpDashService.getLocations().subscribe(
            (locations) => {
                this.locList = locations;
                //Now reorder the loclist array in alphabetical order
                this.locList.sort((a, b) => {
                    return a.name.localeCompare(b.name);
                })

                console.log("This is the locations array: ", this.locList)
            },
            (error) => {
                console.log('Error fetching help info:', error);
            }
        );
    }

    CreateHelp() {
        // First check all the fields are filled in.
        // Remember to neaten up this code. Lots of redundancy and debugging here when trying to figure stuff out.
        if (this.helpForm.valid) {
            // Now into the global.
            // Use type any. type help doesn't work. super weird.
            this.newHelp = {
                Name: this.helpForm.value.Name,
                Description: this.helpForm.value.Description,
                LocationId: this.helpForm.value.Location
            }

            this.helpDashService.createHelp(this.newHelp).subscribe(
                () => {
                    this.GetHelp();
                    this.toastService.success({
                        detail: "SUCCESS",
                        summary: "Help created successfully",
                        duration: 5000
                    })
                },
                (error) => {
                    console.log('Error creating Help:', error);
                }
            );
        }
        this.helpForm.reset();
        this.closeCreateModal();
    }

    UpdateHelp() {
        var helpId = this.editingHelp.helpId;

        // I was giving this editingHelp instead of helpForm.value. And it took me 4 hours to figure that out. dang.
        var tempHelp = {
            Name: this.helpForm.value.Name,
            Description: this.helpForm.value.Description,
            LocationId: this.helpForm.value.Location
        };

        this.helpDashService.updateHelp(helpId, tempHelp).subscribe({
            next: (value) => {
                // console.log('Updating help:', value);
            }, complete: () => {
                this.updateModal.hide();
                this.GetHelp();

                this.toastService.success({
                    detail: "SUCCESS",
                    summary: "Help updated successfully",
                    duration: 5000
                })

            },
            error: (error) => {
                console.log('Error updating Help:', error);
            }
        });
    }

    CancelEdit() {
        this.editingHelp = null;
    }

    DeleteHelp() {
        console.log("Editing help variable: ", this.editingHelp)

        //unable to read id here
        this.helpDashService.deleteHelp(this.editingHelp!.HelpId).subscribe({
            next: (value) => {
                console.log('Deleting Help:', value);
            }, complete: () => {
                this.deleteModal.hide()
                this.GetHelp();
                this.toastService.success({
                    detail: "SUCCESS",
                    summary: "Help deleted successfully",
                    duration: 5000
                })

                // this.deleteToast = true
                // setTimeout(() => {
                //     this.deleteToast = false;
                // }, 2000);
            },
            error: (error) => {
                console.log('Error deleting Help:', error);
            }
        });
    }

    // All modal controls ----------------------//
    showUpdateModal(id: number) {
        // Everything fine with editingHelp and tempLoc
        this.editingHelp = this.helpList.find(x => x.helpId === id);
        console.log(this.editingHelp)

        // For some reason it works better to assign the location as its own object and then manipulate.
        var tempLoc = this.editingHelp.location

        // For populating the select.
        this.GetLocations();

        // These values patch correctly.
        this.helpForm.patchValue({
            Name: this.editingHelp.name,
            Description: this.editingHelp.description,
            Location: tempLoc.locationId
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
        this.editingHelp = this.helpList.find(x => x.helpId === id);
        this.editingHelp!.HelpId = this.helpList.find(x => x.helpId === id).helpId;

        this.editingHelp!.Name = this.helpList.find(x => x.helpId === id).name;

        console.log(this.editingHelp)
        this.deleteModal.show()
    }

    showCreateModal() {
        this.GetLocations();
        this.createModal.show()
    }

    closeCreateModal() {
        this.createModal.hide()
    }


    // Search bar methods ----------------------//
    // Search is not updating when backspacing. Hence add event listener.
    // Means the ngModel is insufficient. On hold for now.
    // onSearchTermChange(value: string) {
    //     this.searchTerm = value;
    // }

    searchHelps() {
        this.helpList = this.helpList.filter(help =>
            help.name.toLowerCase().includes(this.searchTerm.toLowerCase())
        );

        if (this.searchTerm == '') {
            this.helpDashService.getHelp().subscribe(data => {
                this.helpList = data
                console.log(data)
            })
        }
    }

    clearSearch() {
        this.searchTerm = ''
        this.GetHelp()
    }

    // Reporting methods ----------------------//
    // This export is uneccesary for the helps page, but I've put it in just to see how I could streamline it.
    // exportHelps(): void {
    //     const blob = new Blob([JSON.stringify(this.helpList, null, 2)], { type: 'application/json' });
    //     saveAs(blob, 'HelpFile.json');
    // }

    downloadPDF() {
        const doc = new jsPDF();

        const logoSrc = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQa8wwT3d7Q-UCENieoWH3frKQ8-XkQMy6r1utPjjIIoQ&s';
        const title = 'Ignition Help';
        doc.addImage(logoSrc, 'PNG', 80, 20, 55, 30);
        doc.setFontSize(18);
        doc.text(title, 105, 60, { align: 'center' });

        const description = 'This is the help page for ignition. Find answers to your questions on any Ignition functionality here.';
        doc.setFontSize(10);
        doc.setTextColor(100);
        doc.text(description, 20, 70, { align: 'justify' });

        const maxPageHeight = doc.internal.pageSize.height - 20;

        let yPos = 65;
        this.locationPairs.forEach(loc => {
            // Location name
            doc.setFontSize(14);
            doc.setFont('calibri', 'bold');
            doc.text(loc[1], 20, yPos);
            yPos += 10;

            // This is for new page creation to account for overflow
            const remainingPageSpace = maxPageHeight - yPos;
            const itemHeight = 20;

            if (remainingPageSpace < itemHeight) {
                doc.addPage();
                yPos = 20;
            }

            doc.setFontSize(10);
            doc.setFont('calibri', 'bold');
            const helpItems = this.helpList.filter(help => help.locationId === loc[0]);

            helpItems.forEach((help, index) => {
                doc.setFont('calibri', 'bold');
                const helpHeading = help.name;
                doc.text(helpHeading!, 25, yPos);
                yPos += 5;

                doc.setFont('calibri', 'normal');
                const helpText = help.description;
                doc.text(helpText!, 25, yPos);
                yPos += 10;
            });

            yPos += 10;
        });

        doc.save('HelpDocument.pdf');
    }

}