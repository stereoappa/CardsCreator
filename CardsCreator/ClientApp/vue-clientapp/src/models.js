class Card{
      constructor(sideOneText, sideTwoText)
      {
         this.SideOne = new Side(null, sideOneText)
         this.SideTwo = new Side(null, sideTwoText)
         
      }
      get isCompleted () {
         return this.SideOne && this.SideTwo;
      }
}

class Side{
   constructor(languageType, text){
      this.LanguageType = languageType;
      this.Text = text
   }
}