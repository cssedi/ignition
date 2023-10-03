import { AfterViewInit, Component } from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';
import { Prize } from '../../Models/prize';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { PrizeOrderDto } from '../../Models/prize-order-dto';
import { NgTemplateOutlet } from '@angular/common';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-reward-shop',
  templateUrl: './reward-shop.component.html',
  styleUrls: ['./reward-shop.component.scss']
})
export class RewardShopComponent {
  prizes: Prize[]=[];
   prizeOrders : PrizeOrderDto[] = []
   user = { name: '', surname: '', profilePicture: '', userName: '', tokens: 0 };

  cart: any[] = []
  prize : Prize= {
    prizeID: 0,
    name:"",
    description:"",
    frontImgURL:"",
    backImgURL:"",
    price:0,
    prizeTypeID:0
  }
 
  total: number = 0
  isCheckOut : boolean  = false
  isViewPrize: boolean = false
  isViewPrizes : boolean = true
  prizeDetails: Prize={
    prizeID: 0,
    name:"",
    description:"",
    frontImgURL:"",
    backImgURL:"",
    price:0,
    prizeTypeID:0
  }
  cartCount : number = 0
  constructor(private PrizeService: ShopService, private route: Router, private toast:NgToastService) {
    if(localStorage.getItem('cart')){
      var cartList = JSON.parse(localStorage.getItem("cart")!);
      this.cartCount = cartList.length
 
    }
    else{

     this.cartCount = 0
   }

   this.user = JSON.parse(localStorage.getItem('user')!)
   console.log(this.user)
  }

  viewPrize(Id : number){
   this.prize = this.prizes.find( x=> x.prizeID == Id )!
  
   this.isViewPrize = true
   this.isCheckOut = false 
   this.isViewPrizes = false
  }

  addToCart(productId : number){
   if(localStorage.getItem('cart')){
     var cartList = JSON.parse(localStorage.getItem("cart")!);
      cartList.push(productId)
      localStorage.setItem("cart", JSON.stringify(cartList));
      this.cartCount = cartList.length
   }
   else{
    const cartList : any[] = []
    cartList.push(productId)
    localStorage.setItem("cart", JSON.stringify(cartList));
    this.cartCount = cartList.length 
  }

  this.isViewPrize = false
   this.isCheckOut = false 
   this.isViewPrizes = true
  }

  checkOut(){

    this.isCheckOut = true;
    this.isViewPrize = false; 
    this.isViewPrizes = false; 

    var cartList = JSON.parse(localStorage.getItem("cart")!);
    this.prizes.forEach(prize => {
      for(var i = 0; i  < cartList.length; i++){
        if(prize.prizeID == cartList[i]){
          this.cart.push(prize)
          this.total += prize.price
        }
      }
    });

    console.log(this.cart)
    this.cart.forEach(element => {
     
      var prizeOrder : PrizeOrderDto = {
        prizeId : element.prizeID,
      }
      this.prizeOrders.push(prizeOrder)
    });

   
  }

  placeOrder(){
    this.PrizeService.createPrizeOrder(this.prizeOrders).subscribe({ 
      next:(value)  => {
        //remove cart from local storage
        localStorage.removeItem('cart')
        this.toast.success({detail:"SUCCESS",summary:'Order placed successfully!',duration:5000});
        //update token value
        const userToken = JSON.parse(localStorage.getItem('user')!)
        userToken.tokens = value.newTokens
          // Convert the updated object back to JSON
        const updatedUserDataJSON = JSON.stringify(userToken);
        // Save the updated data back to localStorage
        localStorage.setItem('user', updatedUserDataJSON);
        //refresh page 
        setTimeout(function(){ location.reload(); }, 3500);

      }, complete: () => {
        localStorage.removeItem('cart')
        this.isCheckOut = false;
        this.ngOnInit()
      } ,
      error: (err) => {
        console.log('error on create prize order', err.error)
      },
    })
  }
  
  removeFromCart(id : number ){
    var cartList = JSON.parse(localStorage.getItem("cart")!);
    var index = cartList.indexOf(id);
    if (index > -1) {
      cartList.splice(index, 1);
    }
    localStorage.setItem("cart", JSON.stringify(cartList));
    this.cartCount = cartList.length
    this.isViewPrize = false
    this.isCheckOut = false 
    this.isViewPrizes = true
  }

  batoToshop(){ 
    this.isCheckOut = false;
    this.isViewPrize = false; 
    this.isViewPrizes = true; 
  }

  ngOnInit(): void {
    this.PrizeService.GetAllPrizes()
    .subscribe({
      next:(prizes)=>{
        this.prizes=prizes;
        console.log(prizes)
      },
      error: (response)=>{
        console.log(response)
      }

    })
  }

  signOut() {
    localStorage.clear()
    this.route.navigate(['/login']).then(() => {
      location.reload()
    })

  }
}
