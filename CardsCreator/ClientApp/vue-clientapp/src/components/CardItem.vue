<template>
  <div class="card-item" v-bind:class="{'error-card': !card.IsCompleted}">
    <div class="tooltip card-part-short">
      <label>
        <input class="tooltip-toprint" type="checkbox" v-model="card.GoToPrint" />
        <span></span>
      </label>
      <span class="tooltip-toprint">To print</span>
    </div>

    <div class="card-part-long with-right-border"><span class="word">{{card.SideOne.Text}}</span></div>
    <!-- <div class="card-part-short"><strong>{{card.SideOne.LanguageType}}</strong></div> -->

    <div class="card-part-long"><span class="word">{{card.SideTwo.Text}}</span></div>
    <!-- <div class="card-part-short"><strong>{{card.SideTwo.LanguageType}}</strong></div> -->

<div class="card-part-short">
  <!-- <i class="small material-icons">cancel</i> -->
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
  /* padding: 0px 5px; */
  border: 1px solid #ccc;
  border-radius: 18px;
  box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);
  
  display: flex;
  align-items: center;  
  /* align-content: center; */
  justify-content: space-around;

  background-image: url("../assets/img/kraftpaper.jpg");
  background-repeat: no-repeat;
  background-size: 100%;
}

.card-part-short {
  flex-basis: 10%;
  min-width: 0;
}
.card-part-long{
  flex-basis: 40%;
  min-width: 0;
  /* max-width:180px; */
}
.with-right-border{
  border-right: 2px dashed #ccc;
}

.word{
    display:block;
    word-wrap:break-word;
    padding: 5px 5px 5px 0px;

    font-weight: 600;
}

input {
  margin-right: 1rem;
}
.rm {
  background: #d50000;
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
  margin-top: 6px;
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