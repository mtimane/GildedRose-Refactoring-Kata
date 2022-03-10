# -*- coding: utf-8 -*-

BACKSTAGE_NAME = "Backstage passes to a TAFKAL80ETC concert"
LEGENDARY_NAMES = [
    "Sulfuras, Hand of Ragnaros",
]
QUALITY_INCREASE_NAMES = [
    "Aged Brie",
    BACKSTAGE_NAME,
]
TEN_DAYS_OR_LESS = 10
TEN_DAYS_OR_LESS_QUALITY = 2
FIVE_DAYS_OR_LESS = 5
FIVE_DAYS_OR_LESS_QUALITY = 3
CONJURED = "Conjured"
QUALITY_CAP = 50


def add_quality(item, quality=1):
    qualityUnderMax=item.quality < QUALITY_CAP and item.quality+quality < QUALITY_CAP

    if qualityUnderMax:
        item.quality += quality
    else:
        item.quality = QUALITY_CAP
    return item

def remove_quality(item):
    expired = item.sell_in < 0
    conjured = (CONJURED.lower() in item.name.lower())

    if expired and conjured:
        quality = 4
    elif expired or conjured:
        quality = 2
    else:
        quality = 1
    if item.quality > 0 and item.quality-quality > 0:
        item.quality -= quality
    else:
        item.quality = 0
    return item

def add_quality_backstage(item):
    concertEnded=item.sell_in < 0
    concertInFiveDaysLess=item.sell_in <= FIVE_DAYS_OR_LESS
    concertInTenDaysLess=item.sell_in <= TEN_DAYS_OR_LESS

    if concertEnded:
        item.quality = 0
    elif concertInFiveDaysLess:
        item = add_quality(item, FIVE_DAYS_OR_LESS_QUALITY)
    elif concertInTenDaysLess:
        item = add_quality(item, TEN_DAYS_OR_LESS_QUALITY)
    # else:
    #     item = add_quality(item)
    return item
    
class GildedRose(object):

    def __init__(self, items):
        self.items = items

    def update_quality(self):
        for item in self.items:
            
            itemIsNormal=item.name not in QUALITY_INCREASE_NAMES and item.name not in LEGENDARY_NAMES
            itemIsBackstage=item.name == BACKSTAGE_NAME
            itemIsLegendary=item.name in LEGENDARY_NAMES

            if itemIsNormal:
                remove_quality(item)
            elif itemIsBackstage:
                item = add_quality_backstage(item)
            elif not itemIsLegendary:
                add_quality(item)
            if not itemIsLegendary:
                item.sell_in -= 1
class Item:
    def __init__(self, name, sell_in, quality):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)
