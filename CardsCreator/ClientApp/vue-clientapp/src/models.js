export default class Card {
   constructor(sideOneText, sideTwoText) {
      this.SideOne = new Side(1, sideOneText)
      this.SideTwo = new Side(2, sideTwoText)
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