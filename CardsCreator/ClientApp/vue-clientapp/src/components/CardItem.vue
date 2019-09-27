<template>
  <li v-bind:class="{'error-card': !card.IsCompleted}">
    <span>
    <input type="checkbox" v-bind:checked="{'checked':card.GoToPrint}" />
    <strong>{{index + 1}}</strong>
    </span>
    <span>
      <strong>{{card.SideOne.LanguageType}}</strong>
      {{card.SideOne.Text}}
    </span>
    <span>
      <strong>{{card.SideTwo.LanguageType}}</strong>
      {{card.SideTwo.Text}}
    </span>
    <button class="rm" v-on:click="$emit('remove-card', card)">&times;</button>
  </li>
</template>

<script>
export default {
  props: {
    card: {
      type: Object,
      required: true
    },
    index: Number
  }
};
</script>

<style scoped>
li {
  border: 1px solid #ccc;
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 2rem;
  margin-bottom: 0.6rem;
}
input {
  margin-right: 1rem;
}
.rm {
  background: red;
  color: #fff;
  border-radius: 50%;
  font-weight: bold;
}
.error-card {
  border-color: red;
  border-width: 2px;
}
</style>