<template>
  <div id="app">
    <h1>Create your language cards!</h1>
    <hr />

    <p><input v-model="mode" name="addMode" type="radio" value="manuallyMode">Manually added</p>
    <p><input v-model="mode" name="addMode" type="radio" value="parseMode"> Parse your text</p>

    <AddCard @add-card="addCard" v-show="this.mode=='manuallyMode'"/>
    <Parse @parse-cards="parseCards" v-show="this.mode=='parseMode'"/>
    <CardsList v-bind:cards="cards" @remove-card="removeCard" />

    <button v-on:click="generateTable">Get my cards!</button>
  </div>
</template>

<script>
import AddCard from "@/components/AddCard";
import CardsList from "@/components/CardsList";
import Parse from "@/components/Parse";
import { saveAs } from "file-saver";

var mylib = require('models.js');
export default {
  name: "app",
  data() {
    return {
      mode: "",
      cards: [
        new mylib.Card("ad","asd")
        //{
        //  SideOne: { LanguageType: 1, Text: "hello" },
        //  SideTwo: { LanguageType: 2, Text: "привет" },
        //  IsCompleted: false,
        //  GoToPrint: true
        //},
        //{
        //  SideOne: { LanguageType: 1, Text: "work" },
        //  SideTwo: { LanguageType: 2, Text: "работа" },
        //  IsCompleted: true,
        //  GoToPrint: true
        //},
        //{
        //  SideOne: { LanguageType: 1, Text: "home" },
        //  SideTwo: { LanguageType: 2, Text: "дом" },
        //  IsCompleted: true,
        //  GoToPrint: true
        //}
      ]
    };
  },
  mounted() {},
  methods: {
    removeCard(card) {
      this.cards = this.cards.filter(t => t !== card);
    },
    addCard(card) {
      this.cards.push(card);
    },
    parseCards(text){
      console.log(this.mode);
    },
    generateTable() {
      this.$http({
        method: "post",
        responseType: "blob",
        url: "http://api.cardscreator.local/table",
        data: this.cards
      }).then(response => {
          saveAs(response.data, "YourCards.docx");
        }).catch(error => {
          console.log(error);
        }).finally(() => {});
    }
  },
  components: {
    AddCard,
    CardsList,
    Parse
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
