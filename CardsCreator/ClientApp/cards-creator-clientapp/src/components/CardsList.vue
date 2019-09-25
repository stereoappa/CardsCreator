<template>
  <div>
    <ul>
      <CardItem 
      v-for="(card, i) in cards" 
      v-bind:card="card" 
      v-bind:index="i"
      @remove-card="removeCard"/>
    </ul>
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
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}
</style>>
