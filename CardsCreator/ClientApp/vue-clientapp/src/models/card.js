import LanguageType from './languageType.js'

export default class Card {
   constructor(sideOneText = '', sideTwoText = '', 
      langTypeOne = LanguageType.Undefined, 
      langTypeTwo = LanguageType.Undefined) {


      this.SideOne = new Side(langTypeOne, sideOneText)
      this.SideTwo = new Side(langTypeTwo, sideTwoText)
      this.GoToPrint = true; 
   }
   get IsCompleted() {
      return this.SideOne.Text.trim().length > 0 && this.SideTwo.Text.trim().length > 0;
   }
}

class Side {
   constructor(languageType, text) {
      this.LanguageType = languageType;
      this.Text = text
   }
}