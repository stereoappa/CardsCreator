<template>
  <div id="app">
    <h1>Create your language cards!</h1>
    <hr />

    <Creating @add-card="addCard" @parse-cards="parseCards" />

    <select v-model="sortMode">
      <option value="sort-default">In order</option>
      <option value="sort-by-alphabet">By alphabet</option>
      <option value="sort-to-print">To print</option>
    </select>
    <CardsList 
          v-if="sortCards.length"
          v-bind:cards="sortCards" 
          @remove-card="removeCard" 
    />
    <p v-else>No added cards</p>

<div style="display:inline-block">
    <button v-on:click="generateTable" class="btn">Get my cards!</button>
    <Loader v-show="docLoading"/>
</div>
  </div>
</template>

<script>
//components
import Creating from "@/components/Creating"
import CardsList from "@/components/CardsList";
import Loader from "@/components/Loader";

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
      ],
      docLoading: false,
      sortMode:"sort-default"
    };
  },
  mounted() {
    
  },
  computed:{
    sortCards(){
      if(this.sortMode === "sort-by-alphabet"){
        return this.cards.sort((c1, c2) => {return c1.SideOne.Text > c2.SideOne.Text ? 1 : 
                                                   c1.SideOne.Text < c2.SideOne.Text ? -1 : 0})
      }

      if(this.sortMode === "sort-default"){
       return  this.cards;
      }
      //return this.cards;
    }
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
      this.docLoading = true;
      this.$http({
        method: "post",
        responseType: "blob",
        url: AppConfig.apiBaseUri + "table",
        data: this.cards
      }).then(response => {
          saveAs(response.data, "YourCards.docx");
        }).catch(error => {
          this.docLoading = false;
          console.log(error);
        }).finally(() => {this.docLoading = false;});
    }
  },
  components: {
    Creating,
    CardsList,
    Loader
  }
};
</script>

<style>
@import url("http://fonts.googleapis.com/css?family=Roboto");
@import '~materialize-css/dist/css/materialize.min.css';
#app {

  font-family: "Roboto", "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 30px;
}
</style>
