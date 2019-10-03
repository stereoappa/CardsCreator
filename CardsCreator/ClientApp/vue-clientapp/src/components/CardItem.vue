<template>
  <div class="card-item" v-bind:class="{'error-card': !card.IsCompleted}">
    <div class="tooltip card-part-short">
      <label>
        <input class="tooltip-toprint" type="checkbox" v-model="card.GoToPrint" />
        <span></span>
      </label>
      <span class="tooltip-toprint">To print</span>
    </div>

    <div class="card-part-long">{{card.SideOne.Text}}</div>
    <div class="card-part-short"><strong>{{card.SideOne.LanguageType}}</strong></div>

    <div class="card-part-long">{{card.SideTwo.Text}}</div>
    <div class="card-part-short"><strong>{{card.SideTwo.LanguageType}}</strong></div>

<div class="card-part-short">
    <button class="rm" v-on:click="$emit('remove-card', card)">&times;</button>
</div>
  </div>
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

<style>
.card-item {
  flex-basis: 48%;
  border: 1px solid #ccc;
  border-radius: 18px;
  box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);
  display: flex;
  margin-bottom: 0.6rem;
}

.card-part-short {
  /* background-color: antiquewhite; */
  flex-basis: 10%;
}
.card-part-long{
  flex-basis: 30%;
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

/* TOOLTIP */
/* Tooltip container */
.tooltip {
  position: relative;
  display: inline-block;
}

/* Tooltip text */
.tooltip .tooltip-toprint {
  visibility: hidden;
  width: 90px;
  background-color: #555;
  color: #fff;
  text-align: center;
  padding: 5px 0;
  border-radius: 6px;

  /* Position the tooltip text */
  position: absolute;
  z-index: 1;
  bottom: 125%;
  left: 50%;
  margin-left: -38px;

  /* Fade in tooltip */
  opacity: 0;
  transition: opacity 0.2s;
}

/* Tooltip arrow */
.tooltip .tooltip-toprint::after {
  content: "";
  position: absolute;
  top: 100%;
  left: 50%;
  margin-left: -22px;
  border-width: 5px;
  border-style: solid;
  border-color: #555 transparent transparent transparent;
}

/* Show the tooltip text when you mouse over the tooltip container */
.tooltip:hover .tooltip-toprint {
  visibility: visible;
  opacity: 1;
}
</style>