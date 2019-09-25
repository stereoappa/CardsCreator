<template>
  <div id="app">
    <h1>Create your language cards!</h1>
    <AddCard @add-card="addCard" />

    <hr />
    <CardsList v-bind:cards="cards" @remove-card="removeCard" />

    <button>Get my cards!</button>
  </div>
</template>

<script>
import AddCard from "@/components/AddCard";
import CardsList from "@/components/CardsList";
export default {
  name: "app",
  data() {
    return {
      cards: [
        {
          SideOne: { LanguageType: 0, Text: "hello" },
          SideTwo: { LanguageType: 1, Text: "привет" },
          IsCompleted: false,
          GoToPrint: true
        },
        {
          SideOne: { LanguageType: 0, Text: "work" },
          SideTwo: { LanguageType: 1, Text: "работа" },
          IsCompleted: true,
          GoToPrint: true
        },
        {
          SideOne: { LanguageType: 0, Text: "home" },
          SideTwo: { LanguageType: 1, Text: "дом" },
          IsCompleted: true,
          GoToPrint: true
        }
      ]
    };
  },
  mounted() {
    fetch("http")
      .then(response => response.json())
      .then(json => console.log(json));

    this.cards = json;
  },
  methods: {
    removeCard(card) {
      this.cards = this.cards.filter(t => t !== card);
    },
    addCard(card) {
      this.cards.push(card);
    }
  },
  components: {
    AddCard,
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
