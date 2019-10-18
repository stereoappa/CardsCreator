<template>
  <div id="app">
    <header>
      <h1>Create your language cards!</h1>
      <hr />
    </header>

    <Creating @add-card="addCard" @parse-cards="parseCards" v-bind:UIBlocked="UIBlocked" />
    <!-- <a class="btn-floating btn-small waves-effect waves-light red"><i class="material-icons">add</i></a> -->

    <select v-model="sortMode">
      <option value="sort-default">In order</option>
      <option value="sort-by-alphabet">By alphabet</option>
      <option value="sort-to-print">To print</option>
    </select>

    <CardsList v-if="sortCards.length" v-bind:cards="sortCards" @remove-card="removeCard" />
    <p class="no-cards-text" v-else>Cards not yet created..</p>
    <div>
      <div class="control-panel">
        <button class="btn red darken-4" v-if="sortCards.length" v-on:click="clearTable">Clear all</button>
        <button
          v-on:click="generateTable"
          class="btn"
          v-show="sortCards.length"
          :disabled="UIBlocked"
        >Get my cards</button>
        <Loader v-show="docLoading" />
      </div>
    </div>
  </div>
</template>

<script>
//components
import Creating from "@/components/Creating";
import CardsList from "@/components/CardsList";
import Loader from "@/components/Loader";

//dependencies
import AppConfig from "@/vue.config.js";
import { saveAs } from "file-saver";

//models
import Card from "./models/card.js";
import LanguageType from "./models/languageType.js";

export default {
  name: "app",
  data() {
    const storageData = JSON.parse(localStorage.getItem("cards"));
    return {
      cards: storageData
        ? storageData.reduce((cards, obj) => {
            cards.push(Object.assign(new Card(), obj));
            return cards;
          }, [])
        : [],
      docLoading: false,
      UIBlocked: false,
      sortMode: "sort-default"
    };
  },
  mounted() {},
  computed: {
    sortCards() {
      if (this.sortMode === "sort-by-alphabet") {
        return this.cards.sort((c1, c2) => {
          return c1.SideOne.Text > c2.SideOne.Text
            ? 1
            : c1.SideOne.Text < c2.SideOne.Text
            ? -1
            : 0;
        });
      }

      if (this.sortMode === "sort-default") {
        return this.cards;
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
      window.localStorage.setItem("cards", "" + JSON.stringify(this.cards));
    },
    parseCards(text) {
      this.UIBlocked = true;
      var responseCards;
      this.$http({
        method: "post",
        url: AppConfig.apiBaseUri + "parse",
        data: { text: text, separator: "-" }
      })
        .then(response => {
          response.data.forEach(dtoCard => {
            this.cards.push(
              new Card(dtoCard.sideOne.text, dtoCard.sideTwo.text)
            );
          });
        })
        .catch(error => {
          this.UIBlocked = false;
          console.log(error);
        })
        .finally(() => {
          window.localStorage.setItem("cards", "" + JSON.stringify(this.cards));
          this.UIBlocked = false;
        });
    },
    generateTable() {
      this.docLoading = true;
      this.UIBlocked = true;
      this.$http({
        method: "post",
        responseType: "blob",
        url: AppConfig.apiBaseUri + "table",
        data: this.cards
      })
        .then(response => {
          saveAs(response.data, "YourCards.docx");
        })
        .catch(error => {
          this.docLoading = false;
          this.UIBlocked = false;
          console.log(error);
        })
        .finally(() => {
          this.docLoading = false;
          this.UIBlocked = false;
        });
    },
    clearTable() {
      this.cards = [];
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
@import "~materialize-css/dist/css/materialize.min.css";
@import "~material-icons/iconfont/material-icons.css";
header {
  margin-bottom: 40px;
}

body {
  background-image: url("./assets/img/tittle-bg-gray.jpg");
  background-repeat: repeat;
}

#app {
  font-family: "Roboto", "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  /* color: #2c3e50; */
  /* margin-top: 10px; */
}

.no-cards-text {
  font-size: 12pt;
  font-style: italic;
}

.control-panel{
  margin: 0 auto;

  width: 60%;
  display: flex;
  justify-content: space-around;
}

.control-panel button{
  width: 160px;
}
</style>
