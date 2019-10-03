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
    display:flex;
    flex-wrap:wrap;
    flex-direction:row;
    justify-content:space-around;
    align-items:stretch;
}

/* ul {
  list-style: none;
  margin: 0;
  padding: 0;
} */
</style>>
