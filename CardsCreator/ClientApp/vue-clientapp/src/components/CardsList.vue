<template>
  <div>
    <div class="cards-container">
      <CardItem 
      v-for="(card, i) in cards" 
      v-bind:card="card" 
      v-bind:index="i"
      @remove-card="removeCard"/>
    </div>
  </div>
</template>

<script>
import CardItem from "@/components/CardItem";
export default {
  props: ["cards"],
  components: {
    CardItem
  },
  methods:{
    removeCard(card){
        this.$emit('remove-card', card);
    }
  }
};
</script>

<style scoped>
.cards-container{
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(330px, 580px));
    grid-row-gap: 0.6em;

    grid-auto-rows: minmax(20px, auto);
    grid-column-gap: 25px;
    padding: 0px 25px;
    margin-bottom: 15px;

/* grid-auto-rows: minmax(min-content, max-content); */
    /* align-content: stretch; */
    justify-content: center;	
    /* display:flex;
    flex-wrap:wrap;
    flex-direction:row;
    justify-content:space-around; */
    /* align-items:stretch; */
}

/* ul {
  list-style: none;
  margin: 0;
  padding: 0;
} */
</style>>
