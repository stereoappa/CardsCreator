<template>
  <div id="app">
    <h1>Create your language cards!</h1>
    <hr />

    <Creating @add-card="addCard" @parse-cards="parseCards" />

    <CardsList v-bind:cards="cards" @remove-card="removeCard" />

    <button v-on:click="generateTable">Get my cards!</button>
  </div>
</template>

<script>
//components
import Creating from "@/components/Creating"
import CardsList from "@/components/CardsList";

//dependencies
import AppConfig from "@/vue.config.js"
import { saveAs } from "file-saver";

//models
import Card from './models/card.js'
import LanguageType from './models/languageType.js';

export default {
  name: "app",
  data() {
    return {
      cards: [
        new Card("hello", "привет", LanguageType.Eng, LanguageType.Rus)
      ]
    };
  },
  mounted() {
    
  },
  methods: {
    removeCard(card) {
      this.cards = this.cards.filter(t => t !== card);
    },
    addCard(card) {
      this.cards.push(card);
    },
    parseCards(text){
      var responseCards;
      this.$http({
        method: "post",
        url: AppConfig.apiBaseUri + "parse",
        data: {text: text, separator: "-"}
      }).then(response => {
          response.data.forEach(dtoCard => {
            this.cards.push(new Card(dtoCard.sideOne.text, dtoCard.sideTwo.text));
          });
        }).catch(error => {
          console.log(error);
        }).finally(() => {});
    },
    generateTable() {
      this.$http({
        method: "post",
        responseType: "blob",
        url: AppConfig.apiBaseUri + "table",
        data: this.cards
      }).then(response => {
          saveAs(response.data, "YourCards.docx");
        }).catch(error => {
          console.log(error);
        }).finally(() => {});
    }
  },
  components: {
    Creating,
    CardsList
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
