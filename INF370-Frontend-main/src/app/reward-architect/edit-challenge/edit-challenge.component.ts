import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { TokenData } from 'src/app/Models/TokenData';
import { UpdateChallengeDto } from 'src/app/Models/update-challenge-dto';
import { ChallengeService } from 'src/app/services/challenge.service';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';

@Component({
  selector: 'app-edit-challenge',
  templateUrl: './edit-challenge.component.html',
  styleUrls: ['./edit-challenge.component.scss']
})
export class EditChallengeComponent implements OnInit {
  challengeId!: number
  updateChallenge!: any
  challengeForm: FormGroup;
  base64Image: string | null = null;
  constructor(private toast: NgToastService, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private departChalService: DepartementChallengeService, private challengeService: ChallengeService) {
    this.challengeForm = this.formBuilder.group({
      endDate: ['', Validators.required],
      description: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.challengeId = + this.route.snapshot.paramMap.get('challengeId')!;

    this.departChalService.getDapartmentChallenges('jj').subscribe({
      next: (reponse: any[]) => {

        console.log(reponse)
        reponse.forEach(challenge => {
          if (challenge.challengeID == this.challengeId) {
            this.updateChallenge = challenge
            this.base64Image = challenge.image
          }
        });
      }
    })
  }
  onSubmit() {
    if (this.base64Image == null || this.base64Image == '') {

    }
    if (this.challengeForm.valid) {
      // Handle form submission here
      let updateChallenge: UpdateChallengeDto = {
        description: this.challengeForm.value.description,
        endDate: new Date(this.challengeForm.value.endDate),
        Image: this.base64Image!
      }
      this.challengeService.updateChallenge(this.challengeId, updateChallenge).subscribe({
        next: (value) => {
          console.log('on update challenge', value)

        }, complete: () => {
          this.router.navigate(['reward-architect-challenges'])
          this.toast.success({ detail: "SUCCESS", summary: 'Challenge Updated', duration: 5000 });
        },
        error: (err) => {
          console.log('errror on update challenge', err.error)
        },
      })

    } else {
      console.log(this.challengeForm)
      this.challengeForm.markAllAsTouched();
    }
  }
  EditChallenge(challengeId: number) {
    this.router.navigate(['edit-challenge', challengeId])
  }


  clearImageUpload() {
    this.base64Image = null
  }


  processImage(files: FileList | null) {
    if (files && files.length > 0) {
      const file = files[0];
      const allowedExtensions = ['jpg', 'jpeg', 'png', 'gif'];

      const fileExtension = file.name.split('.').pop()?.toLowerCase()!;
      console.log('FILE', fileExtension)
      if (allowedExtensions.includes(fileExtension)) {
        const reader = new FileReader();
        reader.onload = () => {
          this.base64Image = reader.result as string;
        };
        reader.readAsDataURL(file);
      } else {
        console.log('Please select an image file.');
      }
    }
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }

  fileTypeError !: string
  onImageDrop(event: DragEvent) {
    event.preventDefault();
    const droppedFiles = event.dataTransfer?.files;

    if (droppedFiles && droppedFiles.length > 0) {
      const file = droppedFiles[0];
      if (file.type.startsWith('image/')) {
        // The dropped file is an image
        this.processImage(event.dataTransfer?.files!);
      } else {
        // The dropped file is not an image
        this.fileTypeError = "Please select an image file.";
      }
    }
  }


  onDragOver(event: DragEvent) {
    event.preventDefault();
  }
  handleFileInput(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image)
    };

    reader.readAsDataURL(file);
  }
}


