import LanguageType from './languageType.js'

export default class Card {
   constructor(sideOneText, sideTwoText, langTypeOne, langTypeTwo) {
      this.SideOne = new Side(langTypeOne || LanguageType.Undefined, sideOneText)
      this.SideTwo = new Side(langTypeTwo || LanguageType.Undefined, sideTwoText)
      this.GoToPrint = true; 
   }
   get IsCompleted() {
      return this.SideOne.Text.trim() && this.SideTwo.Text.trim();
   }
}

class Side {
   constructor(languageType, text) {
      this.LanguageType = languageType;
      this.Text = text
   }
}